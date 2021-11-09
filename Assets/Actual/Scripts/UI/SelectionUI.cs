using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] private Button spawnMineButton;
    [SerializeField] private Button spawnBarrackButton;
    [SerializeField] private Button spawnWorkerButton;
    [SerializeField] private Button spawnWarriorButton;
    [SerializeField] private Button spawnArcherButton;

    private Player player;
    private void Awake()
    {
        spawnMineButton.onClick.AddListener(onMineClick);

        spawnBarrackButton.onClick.AddListener(onBarrackClick);

        spawnWorkerButton.onClick.AddListener(onWorkerClick);

        spawnWarriorButton.onClick.AddListener(onWarriorClick);

        spawnArcherButton.onClick.AddListener(onArcherClick);
    }
    public void Init(Player player)
    {
        this.player = player;

        spawnMineButton.gameObject.SetActive(player.GetUnitByType(UnitType.MINE) == null);

        spawnBarrackButton.gameObject.SetActive(player.GetUnitByType(UnitType.BARRACK) == null);

        spawnWorkerButton.gameObject.SetActive(true);

        spawnWarriorButton.gameObject.SetActive(player.GetUnitByType(UnitType.BARRACK) != null);

        spawnArcherButton.gameObject.SetActive(player.GetUnitByType(UnitType.BARRACK) != null);
    }
    private void onMineClick()
    {
        var goCastle = player.GetUnitByType(UnitType.CASTLE);
        var radiansMine = goCastle.Rot.eulerAngles.x * Mathf.Deg2Rad;

        var verticalMine = Mathf.Sin(radiansMine);
        var horizontalMine = Mathf.Cos(radiansMine);

        var spawnDirMine = new Vector2(horizontalMine, verticalMine);

        var pos = (Vector2)goCastle.Pos + spawnDirMine * -6;

        SpawnUnit(UnitType.MINE, pos, Quaternion.identity.eulerAngles, player, 10);
    }
    private void onBarrackClick()
    {

        var goCastle = player.GetUnitByType(UnitType.CASTLE);
        var radiansBarrack = goCastle.Rot.eulerAngles.x * Mathf.Deg2Rad;

        var verticalBarrack = Mathf.Sin(radiansBarrack);
        var horizontalBarrack = Mathf.Cos(radiansBarrack);

        var spawnDirBarrack = new Vector2(horizontalBarrack, verticalBarrack);

        var pos = (Vector2)goCastle.Pos + spawnDirBarrack * -2;       //8

        SpawnUnit(UnitType.BARRACK, pos * -1f, Quaternion.identity.eulerAngles, player, 10);
    }

    private void onWorkerClick()
    {
        var goCastle = player.GetUnitByType(UnitType.CASTLE);

        var radiansWorker = goCastle.Rot.eulerAngles.y * Mathf.Deg2Rad;

        var verticalWorker = Mathf.Sin(radiansWorker);
        var horizontalWorker = Mathf.Cos(radiansWorker);

        var spawnDirWorker = new Vector2(horizontalWorker, verticalWorker);

        var pos = (Vector2)goCastle.Pos + spawnDirWorker * 3;

        //var prefabWorker = Resources.Load<WorkerController>("Prefabs/Units/Worker");
        //var controller = GameObject.Instantiate(prefabWorker, pos, Quaternion.Euler(0, 180, 0), gameField);
        //controller.Init();

        SpawnUnit(UnitType.WORKER, pos, Quaternion.identity.eulerAngles, player, 5);
    }
    private void onWarriorClick()
    {
        var barrackTransform = player.GetUnitByType(UnitType.BARRACK);

        var radiansWarrior = barrackTransform.Rot.eulerAngles.y * Mathf.Deg2Rad;

        var verticalWarrior = Mathf.Sin(radiansWarrior);
        var horizontalWarrior = Mathf.Cos(radiansWarrior);

        var spawnDirWarrior = new Vector2(horizontalWarrior, verticalWarrior);

        var pos = (Vector2)barrackTransform.Pos + spawnDirWarrior * 4;

        SpawnUnit(UnitType.WARRIOR, pos * 0.9f, Quaternion.identity.eulerAngles, player, 5);
    }
    private void onArcherClick()
    {
        var barrackTransform = player.GetUnitByType(UnitType.BARRACK);

        var radiansArcher = barrackTransform.Rot.eulerAngles.y * Mathf.Deg2Rad;

        var verticalArcher = Mathf.Sin(radiansArcher);
        var horizontalArcher = Mathf.Cos(radiansArcher);

        var spawnDirArcher = new Vector2(horizontalArcher, verticalArcher);

        var pos = (Vector2)barrackTransform.Pos + spawnDirArcher * 4;

        SpawnUnit(UnitType.ARCHER, pos * 0.9f, Quaternion.identity.eulerAngles, player, 5);
    }

    private IEnumerator DisText()
    {
        GameManager.Data.UIController.Warning.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameManager.Data.UIController.Warning.gameObject.SetActive(false);
    }
    private void SpawnUnit(UnitType type, Vector2 pos, Vector3 rot, Player player, int price)
    {
        if (player.Gold.Value < price)
        {
            GameManager.Data.CoroutineRunner.StartCor(DisText());
            return;
        }

        player.Gold.Value -= price;

        CommandExecutor.Execute(new SpawnUnitData
        {
            Type = type,
            Pos = pos,
            Rot = rot,
            Player = player
        });
        CommandExecutor.Execute(new UpdateSelectionData { Player = player, Unit = null });
    }
}
