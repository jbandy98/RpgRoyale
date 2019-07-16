using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public CombatSocket combatSocket;
    public CombatData combatData;
    public List<CombatCell> combatGridUI;
    public GameObject combatGridGameObject;
    public GameObject combatCellPrefab;
    public GameObject lootWindow;
    public float updateTime = 0.25f;
    public float elapsedTime = 0.0f;
    public Text earnedXp;
    public Text earnedGp;
    public Text earnedAp;
    public string playerId;
    public int gameId;
    public int encounterId;
    public bool combatEnd = false;
    public GameData gameData;
    EnemyManager enemyManager;
    MainGameSceneController gameController;

    private void Awake()
    {
        createGrid();
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameSceneController>();
    }

    public void StartNewCombat(GameData gameData)
    {
        Debug.Log("Starting new combat, enemy encountered!");
        // open websocket for combat
        combatSocket = new CombatSocket();
        combatSocket.StartConnection(this, gameData.playerId);

        combatData = CombatService.startNewCombat(gameData.playerId, gameData.gameId, gameData.combatEncounterId);
        this.gameData = gameData;
        playerId = gameData.playerId;
        gameId = gameData.gameId;
        encounterId = gameData.combatEncounterId;
        combatEnd = false;

        initGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatEnd)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        if (elapsedTime > updateTime && combatData.gameState.Equals("combat"))
        {
            elapsedTime = 0.0f;
            clearUnits();
            foreach (CombatDataUnit unit in combatData.dataUnits)
            {
                GetCellAt(unit.locX, unit.locY).dataUnit = unit;
            }
        }

        if (combatData.gameState.Equals("won"))
        {
            // todo: display won dialog and loot window
            LootData lootData = CombatService.getCombatResults(playerId, gameId, encounterId);
            gameController.combatWindow.SetActive(false);
            lootWindow.SetActive(true);
            earnedXp.text = lootData.xpEarned.ToString();
            earnedGp.text = lootData.gpEarned.ToString();
            earnedAp.text = lootData.apEarned.ToString();
            
            combatEnd = true;
            // remove encounter from game
            enemyManager.encounterObjects.TryGetValue(encounterId, out GameObject encGameObjectToDelete);
            if (encGameObjectToDelete != null)
            {
                Destroy(encGameObjectToDelete);
            }
        } else if (combatData.gameState.Equals("lost"))
        {
            // todo: display lost dialog and remove player from game
            combatEnd = true;
        }

    }

    void createGrid()
    {
        Debug.Log("Creating grid objects...");
        for (int y = 0; y <= 4; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                GameObject newCombatCell = Instantiate(combatCellPrefab, combatGridGameObject.transform);
                CombatCell combatCellData = newCombatCell.GetComponent<CombatCell>();
                combatCellData.dataUnit = new CombatDataUnit();
                combatCellData.locX = x;
                combatCellData.locY = y;
                combatGridUI.Add(combatCellData);
            }
        }
    }

    void initGrid()
    {
        Debug.Log("Initializing grid for combat...");
        for (int y = 0; y <= 4; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                CombatCell combatCellData = GetCellAt(x, y);
                combatCellData.dataUnit = new CombatDataUnit();
            }
        }
    }

    void clearUnits()
    {
        foreach (CombatCell cell in combatGridUI)
        {
            cell.dataUnit = null;
        }
    }

    CombatCell GetCellAt(int x, int y)
    {
        int cellPosition = (y*10) + (x);
        return combatGridUI[cellPosition];
    }

    public void EndLootButton()
    {
        // todo: close loot window
        lootWindow.SetActive(false);
        // todo: re-enable movement
        gameData.gameState = "world";
        GameDataService.updateGameData(gameData);
    }
}
