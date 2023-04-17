using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Boogie;
using Microsoft.Dafny;
using Program = Microsoft.Dafny.Program;

namespace DafnyCore.Fuzzer; 

public class Fuzzer {
  public static Program MainFuzzingLoop(IList<DafnyFile> dafnyFiles, string programName, ErrorReporter reporter, Program dafnyProgram, MutationKernel mutKernel) {
    foreach (var file in dafnyFiles) {
      Console.WriteLine(file.FilePath);
    }

    //TODO: YILAI single file case detection
    if (true) {
      // IList<TopLevelDecl> topLevelDecls = dafnyProgram.DefaultModuleDef.TopLevelDecls;
      // foreach (TopLevelDecl topLevelDecl in topLevelDecls)
      // {
      //   if (topLevelDecl is DefaultClassDecl) {
      //     DefaultClassDecl defaultClassDecl = topLevelDecl as DefaultClassDecl;
      //     foreach (MemberDecl member in defaultClassDecl.Members) {
      //       if (member is Method) {
      //         Method method = member as Method;
      //         List<AttributedExpression> ens = method.Ens;
      //         foreach (AttributedExpression en in ens) {
      //           AttributedExpression mutated = en.Mutate(mutationKernel);
      //         }
      //       } else {
      //         throw new NotImplementedException("Only methods have mutation at the moment");
      //       }
      //     }
      //   } else {
      //     throw new NotImplementedException("Does not handle the case where we have multiple class in a TopLevelDecl in a file");
      //   }
      // }
      // Program mutated = dafnyProgram.mutate(mutKernel);
      // return mutated;
      return dafnyProgram;
    } else {
      //TODO: YILAI multiple files case detection
      ;
    }
    Console.WriteLine("Inside main fuzzing loop");
    
    throw new NotImplementedException();
  }
}