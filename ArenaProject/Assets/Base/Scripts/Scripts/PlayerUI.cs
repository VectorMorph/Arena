using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    RectTransform jetpackFuelFill;

    private PlayerMovement move;
    private Player player;
    private PlayerController controller;
    private WeaponManager weaponManager;

    public void SetMovement(PlayerMovement _move)
    {
        move = _move;
    }

    /*private void Update()
    {
        SetFuelAmount (move.GetJetpackFuel());
    }*/

    void SetFuelAmount(float _amount)
    {
        jetpackFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
    }

}
