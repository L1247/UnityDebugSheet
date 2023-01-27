#region

using System.Collections;
using UnityDebugSheet.Runtime.Core.Scripts;
using UnityDebugSheet.Runtime.Core.Scripts.DefaultImpl.Cells;
using UnityDebugSheet.Runtime.Foundation.PageNavigator;
using UnityEngine;

#endregion

namespace rStarDebugSheet.Scripts
{
    public class DebugSheetController : MonoBehaviour
    {
    #region Private Variables

        private DebugPage rootPage;
        private Page      page;

    #endregion

    #region Unity events

        private void Start()
        {
            // Get or create the root page.
            rootPage = DebugSheet.Instance.GetOrCreateInitialPage(null , OnLoad);

            // Add a link transition to the ExampleDebugPage.
            rootPage.AddPageLinkButton<MyDebugPage>(nameof(MyDebugPage));
        }

    #endregion

    #region Private Methods

        private IEnumerator Init()
        {
            var pageLinkButtonCell = page.GetComponentInChildren<PageLinkButtonCell>();
            Debug.Log($"{pageLinkButtonCell is not null}");
            yield break;
        }

        private void OnLoad(DebugPage debugPage)
        {
            Debug.Log($"onload : {debugPage.name} ");
            page = debugPage;
            page.AddLifecycleEvent(Init);
            // Debug.Log($"{verticalLayoutGroup.name} , {verticalLayoutGroup.transform.childCount}");
            // var firstChild            = verticalLayoutGroup.transform.GetChild(0);
            // EventSystem.current.SetSelectedGameObject(pageLinkButtonCell.gameObject);
        }

    #endregion
    }
}