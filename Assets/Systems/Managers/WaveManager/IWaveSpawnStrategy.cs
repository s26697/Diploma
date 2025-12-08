using System.Collections;

public interface IWaveSpawnStrategy
{
    
    IEnumerator ExecuteSpawn(WaveSO wave);
}
