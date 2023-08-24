public struct GridPosition {
  public int x;
  public int z;

  public GridPosition(int x, int y) {
    this.x = x;
    this.z = y;
  }

  public override string ToString() {
    return "x: " + x + ", z: " + z;
  }

  public static bool isEquals(GridPosition a, GridPosition b) {
    return a.x == b.x && a.z == b.z;
  } 
}