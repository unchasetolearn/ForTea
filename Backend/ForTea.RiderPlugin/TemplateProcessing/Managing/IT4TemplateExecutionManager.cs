using GammaJul.ForTea.Core.Tree;
using JetBrains.Annotations;
using JetBrains.Application.Progress;

namespace JetBrains.ForTea.RiderPlugin.TemplateProcessing.Managing
{
	public interface IT4TemplateExecutionManager
	{
		bool CanExecute([NotNull] IT4File file);
		
		IT4ExecutionResult Execute([NotNull] IT4File file, [CanBeNull] IProgressIndicator progress = null);
	}
}
