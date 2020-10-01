using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{

    [SerializeField]
    private GameObject _turretToBuild;
    public GameObject TurretToBuild
    {
        get
        {
            return _turretToBuild;
        }
        set
        {
            _turretToBuild = value;
        }
    }

    public GameObject BuildTurret()
    {
        return Instantiate(_turretToBuild);
    }


}
