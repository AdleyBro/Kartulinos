using kartstates;
using kart_action;

public static class StatesList
{
    public static OnFloor OnFloor(KartAction k) { return new OnFloor(k); }
    public static InAir InAir(KartAction k) { return new InAir(k); }
    public static DriftingState Drifting(KartAction k) { return new DriftingState(k); }
}
