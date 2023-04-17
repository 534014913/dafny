using System;
using System.Collections.Generic;
using Microsoft.Dafny;

namespace DafnyCore.Fuzzer; 

public class Fuzzer {
  public static void MainFuzzingLoop(IList<DafnyFile> dafnyFiles, string programName, ErrorReporter reporter, Program dafnyProgram) {
    foreach (var file in dafnyFiles) {
      Console.WriteLine(file.FilePath);
    }
    //TODO: single file case detection
    if (true) {
      IList<TopLevelDecl> topLevelDecls = dafnyProgram.DefaultModuleDef.TopLevelDecls;
      foreach (TopLevelDecl topLevelDecl in topLevelDecls)
      {
        if (topLevelDecl is DefaultClassDecl) {
          DefaultClassDecl defaultClassDecl = (DefaultClassDecl)topLevelDecl;
          foreach (MemberDecl member in defaultClassDecl.Members) {
            if (member is Method) {
              Method method = (Method)member;
              List<AttributedExpression> ens = method.Ens;
              foreach (AttributedExpression en in ens) {
                
              }
            } else {
              throw new NotImplementedException("Only methods have mutation at the moment");
            }
          }
        } else {
          throw new NotImplementedException("Does not handle the case where we have multiple class in a TopLevelDecl in a file");
        }
      }
    } else {
      //TODO: multiple files case detection
      ;
    }
    Console.WriteLine("Inside main fuzzing loop");
    
    throw new NotImplementedException();
  }
}