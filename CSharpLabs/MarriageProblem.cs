namespace CSharpLabs;

public class MarriageProblem
{
    private readonly int _contenderNumber;
    private readonly Hall _hall;
    private readonly Princess _princess;
    private const int LevelOfSatisfactionOfChoosingNoOne = 10;

    public MarriageProblem(int contenderNumber, int threshold)
    {
        _contenderNumber = contenderNumber;
        _hall = new Hall(contenderNumber);
        _princess = new Princess(new Freind(), contenderNumber, threshold);
    }

    public MarriageProblem(string file, int contenderNumber, int threshold)
    {
        _contenderNumber = contenderNumber;
        _hall = new Hall(file, contenderNumber);
        _princess = new Princess(new Freind(), contenderNumber, threshold);
    }

    public int SolveProblem()
    {
        int i = 0;
        do
        {
            _princess.ConsiderContender(_hall.Get(i++));
        } while (!_princess.IsChosenOne() && i < _contenderNumber);

        Contender? husband = _princess.GetHusband();
        if (husband == null)
        {
            return LevelOfSatisfactionOfChoosingNoOne;
        }

        return husband.GetScore() > _contenderNumber / 2 ? _princess.GetHusband()!.GetScore() : 0;
    }

    public Hall GetHall()
    {
        return _hall;
    }

}