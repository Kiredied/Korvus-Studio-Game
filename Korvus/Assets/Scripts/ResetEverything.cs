using System.Collections;
using UnityEngine;

public class ResetEverything : MonoBehaviour
{
    [SerializeField] WheelSpin wheelspin;

    [SerializeField] GameObject Player2D;
    static Vector3 OriginalPlayerLocation;
    static Vector3 OriginalPlayerRotation;


    [SerializeField] GameObject CanisterSocket1;
    [SerializeField] GameObject CanisterSocket2;
    [SerializeField] GameObject CanisterSocket3;
    [SerializeField] GameObject CanisterSocket4;

    [SerializeField] GameObject Canister1;
    static Vector3 OriginalCanister1Location;
    static Vector3 OriginalCanister1Rotation;
    
    [SerializeField] GameObject Canister2;
    static Vector3 OriginalCanister2Location;
    static Vector3 OriginalCanister2Rotation;

    [SerializeField] GameObject Canister3;
    static Vector3 OriginalCanister3Location;
    static Vector3 OriginalCanister3Rotation;

    [SerializeField] GameObject SheetSocket1;
    [SerializeField] GameObject SheetSocket2;
    [SerializeField] GameObject SheetSocket3;
    [SerializeField] GameObject DamageSheetSocket1;
    [SerializeField] GameObject DamageSheetSocket2;
    [SerializeField] GameObject DamageSheetSocket3;
    [SerializeField] GameObject DamageSheetSocket4;
    [SerializeField] GameObject DamageSheetSocket5;

    [SerializeField] GameObject Sheet1;
    static Vector3 Sheet1Location;
    static Vector3 Sheet1Rotation;

    [SerializeField] GameObject Sheet2;
    static Vector3 Sheet2Location;
    static Vector3 Sheet2Rotation;

    [SerializeField] GameObject Sheet3;
    static Vector3 Sheet3Location;
    static Vector3 Sheet3Rotation;


    static ResetEverything _instance;

    static public IEnumerator ResetAll()
    {
        ZeppelinDamage.DamageCount = 0;
        DangerZone2D.MountainDeath = false;
        DangerZone2D.DangerZone = false;
        DangerZone2D.DangerWall = false;
        if (_instance != null)
        {
            _instance.CanisterSocket1.SetActive(false);
            _instance.CanisterSocket2.SetActive(false);
            _instance.CanisterSocket3.SetActive(false);
            _instance.CanisterSocket4.SetActive(false);

            _instance.SheetSocket1.SetActive(false);
            _instance.SheetSocket2.SetActive(false);
            _instance.SheetSocket3.SetActive(false);

            _instance.DamageSheetSocket1.SetActive(false);
            _instance.DamageSheetSocket2.SetActive(false);
            _instance.DamageSheetSocket3.SetActive(false);
            _instance.DamageSheetSocket4.SetActive(false);
            _instance.DamageSheetSocket5.SetActive(false);
        }
        yield return new WaitForSeconds(0.1f);

        

        if (_instance != null && _instance.Player2D != null)
        {
            _instance.Player2D.transform.position = OriginalPlayerLocation;
            _instance.Player2D.transform.eulerAngles = OriginalPlayerRotation;
        }
        if (_instance != null && _instance.Canister1 != null)
        {
            _instance.Canister1.transform.position = OriginalCanister1Location;
            _instance.Canister1.transform.eulerAngles = OriginalCanister1Rotation;
        }
        if (_instance != null && _instance.Canister2 != null)
        {
            _instance.Canister2.transform.position = OriginalCanister2Location;
            _instance.Canister2.transform.eulerAngles = OriginalCanister2Rotation;
        }
        if (_instance != null && _instance.Canister3 != null)
        {
            _instance.Canister3.transform.position = OriginalCanister3Location;
            _instance.Canister3.transform.eulerAngles = OriginalCanister3Rotation;
        }
        if (_instance != null && _instance.Sheet1 != null)
        {
            _instance.Sheet1.transform.position = Sheet1Location;
            _instance.Sheet1.transform.eulerAngles = Sheet1Rotation;
        }
        if (_instance != null && _instance.Sheet2 != null)
        {
            _instance.Sheet2.transform.position = Sheet2Location;
            _instance.Sheet2.transform.eulerAngles = Sheet2Rotation;
        }
        if (_instance != null && _instance.Sheet3 != null)
        {
            _instance.Sheet3.transform.position = Sheet3Location;
            _instance.Sheet3.transform.eulerAngles = Sheet3Rotation;
        }
        yield return new WaitForSeconds(0.1f);
        if (_instance != null)
        {
            _instance.CanisterSocket1.SetActive(true);
            _instance.CanisterSocket2.SetActive(true);
            _instance.CanisterSocket3.SetActive(true);
            _instance.CanisterSocket4.SetActive(true);

            _instance.SheetSocket1.SetActive(true);
            _instance.SheetSocket2.SetActive(true);
            _instance.SheetSocket3.SetActive(true);

            _instance.DamageSheetSocket1.SetActive(true);
            _instance.DamageSheetSocket2.SetActive(true);
            _instance.DamageSheetSocket3.SetActive(true);
            _instance.DamageSheetSocket4.SetActive(true);
            _instance.DamageSheetSocket5.SetActive(true);


        }

    }

    void Start()
    {
        _instance = this;
        OriginalPlayerLocation = Player2D.transform.position;
        OriginalPlayerRotation = Player2D.transform.eulerAngles;

        OriginalCanister1Location = Canister1.transform.position;
        OriginalCanister1Rotation = Canister1.transform.eulerAngles;

        OriginalCanister2Location = Canister2.transform.position;
        OriginalCanister2Rotation = Canister2.transform.eulerAngles;

        OriginalCanister3Location = Canister3.transform.position;
        OriginalCanister3Rotation = Canister3.transform.eulerAngles;

        Sheet1Location = Sheet1.transform.position;
        Sheet1Rotation = Sheet1.transform.eulerAngles;

        Sheet2Location = Sheet2.transform.position;
        Sheet2Rotation = Sheet2.transform.eulerAngles;

        Sheet3Location = Sheet3.transform.position;
        Sheet3Rotation = Sheet3.transform.eulerAngles;
    }

    void Update()
    {

    }
}
