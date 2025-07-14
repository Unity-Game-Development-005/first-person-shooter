
using UnityEngine;


public class MinimapController : MonoBehaviour
{
    // reference to player
    public Transform player;



    private void LateUpdate()
    {
        // get position of player in minimap
        Vector3 newMapPosition = player.position;

        // set 'y' position of minimap
        newMapPosition.y = transform.position.y;

        // set new position of minimap
        transform.position = newMapPosition;

        // rotate map in direction of player
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

} // end of class
