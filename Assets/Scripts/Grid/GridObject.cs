public class GridObject {
    private GridPosition position;
    private GridSystem gridSystem;
    
    public GridObject(GridSystem gridSystem, GridPosition position) {
        this.gridSystem = gridSystem;
        this.position = position;
    }
}