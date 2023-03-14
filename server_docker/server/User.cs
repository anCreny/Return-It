



public class User: IComparable<User>
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public int Score { get; set; }
    
    public int CompareTo(User? user)
    {
        if (user == null) throw new ArgumentException("Invalid parameter value");
        return Score.CompareTo(user.Score);

    }
}



