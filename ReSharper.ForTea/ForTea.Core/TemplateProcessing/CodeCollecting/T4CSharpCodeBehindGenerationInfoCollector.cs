using System.Text;
using GammaJul.ForTea.Core.Psi;
using GammaJul.ForTea.Core.Psi.Directives;
using GammaJul.ForTea.Core.Tree;
using JetBrains.Annotations;

namespace GammaJul.ForTea.Core.TemplateProcessing.CodeCollecting
{
	public sealed class T4CSharpCodeBehindGenerationInfoCollector : T4CSharpCodeGenerationInfoCollectorBase
	{
		internal const string CodeCommentStart = "/*_T4\x200CCodeStart_*/";
		internal const string CodeCommentEnd = "/*_T4\x200CCodeEnd_*/";

		public T4CSharpCodeBehindGenerationInfoCollector(
			[NotNull] IT4File file,
			[NotNull] T4DirectiveInfoManager manager
		) : base(file, manager)
		{
		}

		protected override string ToStringConversionStart => "__ToString(";

		protected override void AppendCode(T4CSharpCodeGenerationResult result, IT4Token token)
		{
			result.Builder.Append(CodeCommentStart);
			result.AppendMapped(token);
			result.Builder.Append(CodeCommentEnd);
		}
	}
}