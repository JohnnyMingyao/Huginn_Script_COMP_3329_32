

using UnityEngine;
using UnityEngine.UI;
public class BarManager : MonoBehaviour
{
    private GameObject player;
    public Image dashBar;
    public Image energyBar;
    private float maxEnergy;
    private int maxDashes;
    private
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxDashes = player.GetComponent<PlayerMove>().getMaxDashes();
        maxEnergy = player.GetComponent<PlayerMove>().getMaxFlyForce();
    }

    // Update is called once per frame
    void Update()
    {
        float energy = player.GetComponent<PlayerMove>().getFlyForce();
        float avaliableDashes = player.GetComponent<PlayerMove>().getDashes();
        energyBar.fillAmount = energy / maxEnergy;
        dashBar.fillAmount = avaliableDashes / maxDashes;
    }
}
