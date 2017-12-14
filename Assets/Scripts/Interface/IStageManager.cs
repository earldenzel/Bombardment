public interface IStage {

    bool FirstEnter { get; }
    void OnEnter();
    void OnStage();
    void OnExit();

}
