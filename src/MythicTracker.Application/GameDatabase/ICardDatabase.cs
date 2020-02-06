namespace MythicTracker.Application.GameDatabase
{
    public interface ICardDatabase
    {
        Card GetCard(int id);

        Card[] GetAllCards();
    }
}
