using System;
using System.Collections.Generic;
using CLSTools.CSInterpreter;

namespace CLSTools.Examples{
  public static class Program{
    public static void Main(){
      Dictionary<string, Namespace> program = new Dictionary<string, Namespace>();
      program["SM64Code"] = new Namespace("Booter", new Class("SM64Actions"));
      program["SM64Code"].ParentProgram.Usings.Add(new Reference("SM64", ReferenceTypes.Abstract));
      program["SM64Code"].Classes["Booter"].Methods.Add("OnFileLoad", new Method("void", "char", "int"));
      Method meth = program["SM64Code"].Classes["Booter"].Methods["OnFileLoad"];
      meth.NameParameters("file", "stars");
      meth.Commands.Add(new Command("if (stars == 0) Screen.Print(\"Welcome new player\" + Symbols.Star.ToString());"));
    }
  }
}