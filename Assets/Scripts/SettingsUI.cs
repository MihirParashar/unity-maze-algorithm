using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    private int width;
    private int height;
    private int seed;
    private float tickInterval;
    private Coroutine mazeCoroutine;

    [SerializeField] private Slider widthSlider;
    [SerializeField] private TextMeshProUGUI widthText;
    [SerializeField] private Slider heightSlider;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TMP_InputField seedInput;
    [SerializeField] private TMP_InputField tickIntervalInput;
    [SerializeField] private MazeGenerator mazeGenerator;

    private void Start()
    {
        GetDataFromSave();
    }

    public void UpdateWidthSlider(float newValue)
    {
        widthText.text = newValue.ToString();
        width = (int)newValue;
    }
    public void UpdateHeightSlider(float newValue)
    {
        heightText.text = newValue.ToString();
        height = (int)newValue;
    }

    public void UpdateSeedInput(string newValue)
    {
        seedInput.text = newValue;
        seed = Mathf.Clamp(int.Parse(newValue), int.MinValue, int.MaxValue);
    }

    public void UpdateTickIntervalInput(string newValue)
    {
        tickIntervalInput.text = newValue;
        tickInterval = Mathf.Clamp(float.Parse(newValue), float.MinValue, float.MaxValue);
    }

    public void GenerateRandomSeed()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
        UpdateSeedInput(seed.ToString());
    }

    public void Generate()
    {
        if (mazeCoroutine != null)
        {
            mazeGenerator.StopCoroutine(mazeCoroutine);
        }

        mazeCoroutine = StartCoroutine(mazeGenerator.GenerateMaze(width, height, seed, tickInterval));
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }


    void OnApplicationQuit()
    {
        SaveInputData();
    }

    void SaveInputData()
    {
        PlayerPrefs.SetInt("Width", width);
        PlayerPrefs.SetInt("Height", height);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.SetFloat("TickInterval", tickInterval);
    }

    void GetDataFromSave()
    {
        width = PlayerPrefs.GetInt("Width", 10);
        widthSlider.value = width;
        widthText.text = width.ToString();

        height = PlayerPrefs.GetInt("Height", 10);
        heightSlider.value = height;
        heightText.text = height.ToString();

        seed = PlayerPrefs.GetInt("Seed", 0);
        seedInput.text = seed.ToString();

        tickInterval = PlayerPrefs.GetFloat("TickInterval", 0.01f);
        tickIntervalInput.text = tickInterval.ToString();
    }
}
