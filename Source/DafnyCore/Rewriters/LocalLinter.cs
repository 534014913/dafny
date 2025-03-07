//-----------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All Rights Reserved.
// Copyright by the contributors to the Dafny Project
// SPDX-License-Identifier: MIT
//
//-----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Microsoft.Dafny {

  public class LocalLinter : IRewriter {
    internal override void PostResolveIntermediate(ModuleDefinition moduleDefinition) {
      base.PostResolveIntermediate(moduleDefinition);
      foreach (var topLevelDecl in moduleDefinition.TopLevelDecls.OfType<TopLevelDeclWithMembers>()) {
        foreach (var callable in topLevelDecl.Members.OfType<ICallable>()) {
          var visitor = new UselessOldLinterVisitor(Reporter);
          visitor.Visit(callable, Unit.Default);
        }
      }
    }

    public LocalLinter(ErrorReporter reporter) : base(reporter) {
    }
  }

  class UselessOldLinterVisitor : TopDownVisitor<Unit> {
    private readonly ErrorReporter reporter;

    public UselessOldLinterVisitor(ErrorReporter reporter) {
      this.reporter = reporter;
    }

    protected override bool VisitOneExpr(Expression expr, ref Unit st) {
      if (expr is OldExpr oldExpr && !AutoGeneratedToken.Is(oldExpr.tok)) {
        var fvs = new HashSet<IVariable>();
        var usesHeap = false;
        FreeVariablesUtil.ComputeFreeVariables(reporter.Options, oldExpr.E, fvs, ref usesHeap, true);
        if (!usesHeap) {
          oldExpr.Useless = true;
          this.reporter.Warning(MessageSource.Rewriter, ErrorRegistry.NoneId, oldExpr.tok,
            $"Argument to 'old' does not dereference the mutable heap, so this use of 'old' has no effect");
        }
      }
      return base.VisitOneExpr(expr, ref st);
    }

    protected override bool VisitOneStmt(Statement stmt, ref Unit st) {
      // Check if the statement has the three tokens "assert", "!", and "(" next to each other. If so, that may
      // be a mistake, especially if the programmer is coming from Rust.
      if (stmt is AssertStmt { Expr: UnaryOpExpr { Op: UnaryOpExpr.Opcode.Not, E: ParensExpression } negationExpression }) {
        if (stmt.tok.pos + 6/*="assert".Length*/ == negationExpression.tok.pos) {
          reporter.Warning(MessageSource.Rewriter, ErrorRegistry.NoneId, stmt.tok,
            "You have written an assert statement with a negated condition, but the lack of whitespace between 'assert' and '!' " +
            "suggests you may be used to Rust and have accidentally negated the asserted condition. If you did not intend the negation, " +
            "remove the '!' and the parentheses; if you do want the negation, please add a space between the 'assert' and '!'.");
        }
      }
      return base.VisitOneStmt(stmt, ref st);
    }
  }
}
