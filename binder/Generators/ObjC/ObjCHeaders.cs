﻿using CppSharp.AST;
using CppSharp.Generators;

namespace MonoEmbeddinator4000.Generators
{
    public class ObjCHeaders : CHeaders
    {
        public ObjCHeaders(BindingContext context, Options options,
            TranslationUnit unit)
            : base(context, options, unit)
        {
        }

        public override void Process()
        {
            base.Process();
        }

        public override void WriteHeaders()
        {
            base.WriteHeaders();
            WriteLine("#import <Foundation/Foundation.h>");
        }

        public override void GenerateMethodSignature(Method method,
            bool isSource)
        {
            this.GenerateObjCMethodSignature(method);
        }

        public override bool VisitClassDecl(Class @class)
        {
            PushBlock();

            Write("@interface {0} : NSObject", @class.QualifiedName);

            var hasFields = @class.Fields.Count != 0;
            if (hasFields)
            {
                Write(" ");
                WriteStartBraceIndent();

                foreach (var field in @class.Fields)
                    field.Visit(this);

                WriteCloseBraceIndent();
                NewLine();
            }
            
            VisitDeclContext(@class);

            WriteLine("@end");

            PopBlock(NewLineKind.BeforeNextBlock);

            return true;
        }
    }
}
