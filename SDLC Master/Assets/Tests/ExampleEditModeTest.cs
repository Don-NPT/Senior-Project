using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using Project;


namespace Tests.EditMode
{
    public class ProjectTest
    {
        [Test]
        public void Startproject()
        {
            Project project = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project001.asset");
            int expectedProjectId = 1;

            int actualProjectId = project.projectId;

            Assert.AreEqual(expectedProjectId, actualProjectId);
        }

        [Test]
        public void CheckProjectInProgressTest_WithProjectInProgress_ReturnTrue()
        {
            ProjectManager manager = new GameObject().AddComponent<ProjectManager>();
            manager.allProjects = new Project[2];
            manager.allProjects[0] = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project001.asset");
            manager.allProjects[0].inProgress = false;
            manager.allProjects[1] = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project002.asset");
            manager.allProjects[1].inProgress = true;

            bool isInProgress = manager.CheckProjectInProgress();

            Assert.IsTrue(isInProgress);
        }

        [Test]
        public void CheckProjectInProgressTest_WithNoProjectInProgress_ReturnFalse()
        {
            ProjectManager manager = new GameObject().AddComponent<ProjectManager>();
            manager.allProjects = new Project[2];
            manager.allProjects[0] = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project001.asset");
            manager.allProjects[0].inProgress = false;
            manager.allProjects[1] = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project002.asset");
            manager.allProjects[1].inProgress = false;

            bool isInProgress = manager.CheckProjectInProgress();

            Assert.IsFalse(isInProgress);
        }

        [Test]
        public void FinishProject_ShouldAddProjectToOldProjectList()
        {
            GameObject go = new GameObject();
            ProjectManager manager = go.AddComponent<ProjectManager>();
            Project project = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project001.asset");
            manager.currentProject = project;

            Assert.IsNotNull(project, "Project is null.");

            manager.FinishProject(project);

            Assert.IsNull(manager.currentProject);
            Assert.Contains(project, manager.oldProject);
            GameObject.DestroyImmediate(go);
        }

        [Test]
        public void FinishProject_ShouldUpdateActualQuality()
        {
            GameObject go = new GameObject();
            ProjectManager manager = go.AddComponent<ProjectManager>();
            Project project = AssetDatabase.LoadAssetAtPath<Project>("Assets/Projects/Project001.asset");
            manager.currentProject = project;

            manager.FinishProject(project);

            Assert.IsNull(manager.currentProject);

            Assert.IsNotNull(project.actualAnalysis);
            Assert.IsNotNull(project.actualDesign);
            Assert.IsNotNull(project.actualCoding);
            Assert.IsNotNull(project.actualTesting);
            Assert.IsNotNull(project.actualDeployment);

            GameObject.DestroyImmediate(go);
        }
    }
}
