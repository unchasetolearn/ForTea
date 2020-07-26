using System.Collections.Generic;
using GammaJul.ForTea.Core.Parsing.Ranges;
using GammaJul.ForTea.Core.TemplateProcessing.CodeCollecting;
using GammaJul.ForTea.Core.TemplateProcessing.CodeCollecting.Descriptions;
using GammaJul.ForTea.Core.TemplateProcessing.CodeGeneration;
using GammaJul.ForTea.Core.TemplateProcessing.CodeGeneration.Converters;
using GammaJul.ForTea.Core.TemplateProcessing.CodeGeneration.Converters.ClassName;
using GammaJul.ForTea.Core.Tree;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;

namespace JetBrains.ForTea.RiderPlugin.TemplateProcessing.CodeGeneration.Converters
{
	public class T4CSharpRealIntermediateConverter : T4CSharpIntermediateConverterBase
	{
		public T4CSharpRealIntermediateConverter(
			[NotNull] IT4File file,
			[NotNull] IT4GeneratedClassNameProvider classNameProvider
		) : base(file, classNameProvider)
		{
		}

		protected sealed override string BaseClassResourceName => "GammaJul.ForTea.Core.Resources.TemplateBaseFull.cs";

		protected sealed override void AppendParameterInitialization(
			IReadOnlyCollection<T4ParameterDescription> descriptions
		)
		{
			AppendIndent();
			Result.AppendLine("if (Errors.HasErrors) return;");
			foreach (var description in descriptions)
			{
				AppendIndent();
				Result.Append("if (Session.ContainsKey(nameof(");
				Result.Append(description.FieldNameString);
				Result.AppendLine(")))");
				AppendIndent();
				Result.AppendLine("{");
				PushIndent();
				AppendIndent();
				Result.Append(description.FieldNameString);
				Result.Append(" = (");
				description.AppendType(Result);
				Result.Append(") Session[nameof(");
				Result.Append(description.FieldNameString);
				Result.AppendLine(")];");
				PopIndent();
				AppendIndent();
				Result.AppendLine("}");
				AppendIndent();
				Result.AppendLine("else");
				AppendIndent();
				Result.AppendLine("{");
				PushIndent();
				AppendIndent();
				Result.Append(
					"object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData(nameof(");
				Result.Append(description.FieldNameString);
				Result.AppendLine("));");
				AppendIndent();
				Result.AppendLine("if (data != null)");
				AppendIndent();
				Result.AppendLine("{");
				PushIndent();
				AppendIndent();
				Result.Append(description.FieldNameString);
				Result.Append(" = (");
				description.AppendType(Result);
				Result.AppendLine(") data;");
				PopIndent();
				AppendIndent();
				Result.AppendLine("}");
				PopIndent();
				AppendIndent();
				Result.AppendLine("}");
			}
		}

		protected override void AppendClass(T4CSharpCodeGenerationIntermediateResult intermediateResult)
		{
			AppendIndent();
			Result.AppendLine();
			AppendClassSummary();
			AppendIndent();
			Result.AppendLine();
			AppendIndent();
			Result.AppendLine($"#line 1 \"{File.GetSourceFile().GetLocation()}\"");
			AppendIndent();
			Result.AppendLine(
				"[global::System.CodeDom.Compiler.GeneratedCodeAttribute(\"JetBrains.ForTea.TextTemplating\", \"42.42.42.42\")]");
			base.AppendClass(intermediateResult);
		}

		protected override void AppendTransformMethod(T4CSharpCodeGenerationIntermediateResult intermediateResult)
		{
			Result.AppendLine("#line hidden");
			AppendIndent();
			Result.AppendLine("/// <summary>");
			AppendIndent();
			Result.AppendLine("/// Create the template output");
			AppendIndent();
			Result.AppendLine("/// </summary>");
			base.AppendTransformMethod(intermediateResult);
		}

		private void AppendClassSummary()
		{
			AppendIndent();
			Result.AppendLine("/// <summary>");
			AppendIndent();
			Result.AppendLine("/// Class to produce the template output");
			AppendIndent();
			Result.AppendLine("/// </summary>");
		}

		public override T4CSharpCodeGenerationResult Convert(
			T4CSharpCodeGenerationIntermediateResult intermediateResult
		)
		{
			Result.Append(@"// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by JetBrains T4 Processor
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
");
			return base.Convert(intermediateResult);
		}

		protected override void AppendParameterDeclaration(T4ParameterDescription description)
		{
			// Range maps of this converter are ignored, so it's safe to use Append instead of AppendMapped
			AppendIndent();
			Result.Append("private ");
			description.AppendTypeMapped(Result);
			Result.Append(" ");
			description.AppendName(Result);
			Result.Append(" => ");
			Result.Append(description.FieldNameString);
			Result.AppendLine(";");
		}

		protected override void AppendHost()
		{
			// Host directive does not work for runtime templates
		}

		protected override void AppendIndent(int size)
		{
			// TODO: use user indents?
			for (int index = 0; index < size; index += 1)
			{
				Result.Append("    ");
			}
		}

		protected override string BaseClassDescription =>
			@"     #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute(""JetBrains.ForTea.TextTemplating"", ""42.42.42.42"")]";

		#region IT4ElementAppendFormatProvider
		public override string CodeCommentStart => "";
		public override string CodeCommentEnd => "";
		public override string ExpressionCommentStart => "";
		public override string ExpressionCommentEnd => "";
		public override string Indent => new string(' ', CurrentIndent * 4); // TODO: use user indents?
		public override bool ShouldBreakExpressionWithLineDirective => false;

		public override void AppendCompilationOffset(T4CSharpCodeGenerationResult destination, IT4TreeNode node)
		{
			// In preprocessed file, behave like VS
		}

		public override void AppendLineDirective(T4CSharpCodeGenerationResult destination, IT4TreeNode node)
		{
			var sourceFile = node.FindLogicalPsiSourceFile();
			int offset = T4UnsafeManualRangeTranslationUtil.GetDocumentStartOffset(node).Offset;
			int line = (int) sourceFile.Document.GetCoordsByOffset(offset).Line;
			destination.AppendLine($"#line {line + 1} \"{sourceFile.GetLocation()}\"");
		}

		public override void AppendMappedIfNeeded(T4CSharpCodeGenerationResult destination, IT4Code code) =>
			destination.Append(code.GetText().Trim());
		#endregion IT4ElementAppendFormatProvider
	}
}
