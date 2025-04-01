
public class SongEvent {
    public float time;
    public Level.SongAction[] actions;

    public SongEvent(float time, Level.SongAction[] actions) {
        this.time = time;
        this.actions = actions;
    }
}