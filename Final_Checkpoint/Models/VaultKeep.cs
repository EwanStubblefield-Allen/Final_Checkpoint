namespace Final_Checkpoint.Models;

public class VaultKeep : RepoItem<int>
{
  public string CreatorId { get; set; }
  public int KeepId { get; set; }
  public int VaultId { get; set; }
}