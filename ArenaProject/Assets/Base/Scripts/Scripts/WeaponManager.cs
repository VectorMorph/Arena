using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    private PlayerWeapon primaryWeapon;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private string weaponLayerName = "Weapon";
    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;

    private void Start()
    {
        EquipWeapon(primaryWeapon);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponInst = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponInst.transform.SetParent(weaponHolder);

        currentGraphics = _weaponInst.GetComponent<WeaponGraphics>();
        if(currentGraphics == null)
        {
            Debug.LogError("No WeaponGraphics compononet on " + _weaponInst.name);
        }

        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponInst, LayerMask.NameToLayer(weaponLayerName));
        }

    }

}
