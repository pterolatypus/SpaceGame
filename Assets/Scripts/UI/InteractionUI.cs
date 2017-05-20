using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour {

    public PlayerShipController player { get; set; }
    private List<InteractionTab> tabs;
    private static GameObject button;
    [SerializeField]
    private GameObject tabButtonHolder;
    private CameraFollowPlayer playerCamera;
    public Orbital source { get; set; }
    private GameObject worldUI;

    public void Awake() {
        if (button == null) {
            button = Resources.Load("Prefabs/UI/TabButton") as GameObject;
        }
        tabs = new List<InteractionTab>();
        playerCamera = Camera.main.GetComponent<CameraFollowPlayer>();
    }

    public void Start() {
        player.EnableControls(false);
        Vector3 planetWorldPos = source.gameObject.transform.position;
        playerCamera.LockToPosition(planetWorldPos);
        Vector3 planetPos = Camera.main.WorldToScreenPoint(planetWorldPos);
        planetPos.x += 170;
        Vector3 lockPos = Camera.main.ScreenToWorldPoint(planetPos);
        playerCamera.LockToPosition(lockPos, 2f);
        worldUI = GameObject.Find("InWorldUI");
        worldUI.SetActive(false);
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this.Close();
        }
    }

	public void Close() {
        GameObject.Destroy(this.gameObject);
        player.EnableControls(true);
        playerCamera.LockToPosition(null);
        worldUI.SetActive(true);
    }

    public void AddTab(OrbitalInteraction interaction) {
        InteractionTab tab = interaction.GetTab();
        tabs.Add(tab);
        if (tabs.Count > 1) {
            tab.gameObject.SetActive(false);
        }
        RectTransform t = (RectTransform)tab.transform;
        t.SetParent(this.transform, false);
        TabButton button = GameObject.Instantiate(InteractionUI.button).GetComponent<TabButton>();
        button.Bind(this, tab);
        button.text = tab.GetTitle();
        button.transform.SetParent(tabButtonHolder.transform, false);
        ((RectTransform)button.transform).localPosition = new Vector2(10 + (170*(tabs.Count-1)), 0);
    }

    private void ClearTabs() {
        foreach (InteractionTab tab in tabs) {
            tab.gameObject.SetActive(false);
        }
    }

    public void SetActiveTab(InteractionTab tab) {
        ClearTabs();
        tab.gameObject.SetActive(true);
    }

}
