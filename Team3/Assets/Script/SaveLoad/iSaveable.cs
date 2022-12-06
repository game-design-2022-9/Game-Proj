

public interface iSaveable 
{
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    GameSaveData GenerateSaveData();

    void RestoreGameData(GameSaveData saveData);

}
