public class UnitProcess {
    public void Initialize() {
        EventController.Event.On<SpawnUnit>(OnSpawnUnit);
    }
    public void Release() {
        EventController.Event.Off<SpawnUnit>(OnSpawnUnit);
    }

    public void OnSpawnUnit(SpawnUnit e) {
        e.gameObject.AddComponent<BaseUnitModel>();
    }
}