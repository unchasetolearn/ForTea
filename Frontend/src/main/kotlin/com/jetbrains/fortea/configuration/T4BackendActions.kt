package com.jetbrains.fortea.configuration

import com.intellij.openapi.actionSystem.AnActionEvent
import com.intellij.openapi.actionSystem.CommonDataKeys
import com.jetbrains.fortea.language.T4Language
import com.jetbrains.rider.actions.base.RiderContextAwareAnAction
import com.jetbrains.rider.icons.ReSharperCommonIcons.Debug
import com.jetbrains.rider.icons.ReSharperLiveTemplatesCSharpIcons.ScopeCS
import com.jetbrains.rider.icons.ReSharperPsiBuildScriptsIcons.Run
import com.jetbrains.rider.model.T4FileLocation
import com.jetbrains.rider.model.t4ProtocolModel
import com.jetbrains.rider.projectView.ProjectModelViewHost
import com.jetbrains.rider.projectView.solution
import javax.swing.Icon

abstract class T4BackendAction(backendActionId: String, icon: Icon) :
  RiderContextAwareAnAction(backendActionId, icon = icon) {
  override fun update(e: AnActionEvent) {
    val dataContext = e.dataContext
    val presentation = e.presentation
    val psiFile = dataContext.getData(CommonDataKeys.PSI_FILE)
    if (psiFile?.language != T4Language) {
      presentation.isEnabledAndVisible = false
      return
    }

    presentation.isVisible = true
    presentation.isEnabled = false

    val project = e.project ?: return
    val host = ProjectModelViewHost.getInstance(project)
    val item = host.getItemsByVirtualFile(psiFile.virtualFile).singleOrNull() ?: return
    val canExecute = project.solution.t4ProtocolModel.canExecute.sync(T4FileLocation(item.id))
    if (canExecute) presentation.isEnabled = true
  }
}

class T4ExecuteTemplateBackendAction : T4BackendAction("T4.ExecuteFromContext", Run)
class T4DebugTemplateBackendAction : T4BackendAction("T4.DebugFromContext", Debug)
class T4PreprocessTemplateBackendAction : T4BackendAction("T4.PreprocessFromContext", ScopeCS)