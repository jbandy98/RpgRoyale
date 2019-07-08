using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    public GameData gameData;
    public Encounter encounter;
    public CombatSocket combatSocket;
    public CombatData combatData;
    public List<CombatCell> combatGridUI;
    public GameObject combatGridGameObject;
    public GameObject combatCellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNewCombat(GameData gameData)
    {
        // open websocket for combat
        combatSocket = new CombatSocket();
        combatSocket.StartConnection(this, gameData);
        this.gameData = gameData;
        // find enemy that triggered combat and start a new combat with them and the player
        this.encounter = EnemyService.findEncounterAtPlayerLoc(gameData.gameId, gameData.playerId);
        CombatService.startNewCombat(gameData.playerId, gameData.gameId, encounter.encounterId);
        createGrid();
    }

    // Update is called once per frame
    void Update()
    {
        clearUnits();
        foreach(CombatUnit unit in combatData.allUnits) {
            GetCellAt(unit.location.intX, unit.location.intY).combatUnit = unit;
        }
    }

    void createGrid()
    {
        for (int y = 4; y >= 0; y--)
        {
            for (int x = 0; x < 10; x++)
            {
                GameObject newCombatCell = Instantiate(combatCellPrefab, combatGridGameObject.transform);
                CombatCell combatCellData = newCombatCell.GetComponent<CombatCell>();
                combatCellData.locX = x;
                combatCellData.locY = y;
            }
        }
    }

    void clearUnits()
    {
        foreach (CombatCell cell in combatGridUI)
        {
            cell.combatUnit = null;
        }
    }

    CombatCell GetCellAt(int x, int y)
    {
        int cellPosition = (4 - y + 1) * (x + 1) - 1;
        return combatGridUI[cellPosition];
    }
}
