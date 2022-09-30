namespace Hangman.Game.Presenters
{

    public class CharacterInputPresenter
    {
        public void OnCharacerEntered(string character)
        {
            if (!string.IsNullOrEmpty(character))
            {
                UnityEngine.Debug.Log(character);
            }
        }
    }    
}
