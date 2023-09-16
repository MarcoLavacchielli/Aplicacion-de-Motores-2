using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface ILifeObserver
{
    void OnLifeChanged(int newLife);
}
public class PlayerLife : MonoBehaviour, ILifeObserver
{
    public int startingLife = 10; // Puedes ajustar esto seg�n tus necesidades.
    private int life;
    public int Life
    {
        get { return life; }
        private set
        {
            life = value;
            NotifyLifeChange();

            // Verifica si la vida llega a cero o menos.
            if (life <= 0)
            {
                // Llama a un m�todo para reiniciar la escena.
                RestartScene();
            }
        }
    }
    private void RestartScene()
    {
        // Obt�n el �ndice de la escena actual.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Carga la escena actual para reiniciarla.
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Lista de observadores (listeners) que se notificar�n cuando cambie la vida.
    private List<ILifeObserver> lifeObservers = new List<ILifeObserver>();

    private void Start()
    {
        Life = startingLife; // Inicializa la vida del jugador al valor inicial.
    }

    public void AddLifeObserver(ILifeObserver observer)
    {
        lifeObservers.Add(observer);
    }

    public void RemoveLifeObserver(ILifeObserver observer)
    {
        lifeObservers.Remove(observer);
    }

    private void NotifyLifeChange()
    {
        foreach (var observer in lifeObservers)
        {
            observer.OnLifeChanged(Life);
        }
    }

    // M�todo para recibir da�o.
    public void TakeDamage(int damageAmount)
    {
        Life -= damageAmount;

        // Puedes agregar l�gica adicional aqu�, como verificar si el jugador muri�.
    }

    // M�todo para obtener curaci�n.
    public void Heal(int healAmount)
    {
        Life += healAmount;
    }

    public void OnLifeChanged(int newLife)
    {
        // Este m�todo se llama cuando cambia la vida del jugador,
        // pero no necesitas hacer nada espec�fico aqu� para el jugador.
        // Si deseas reaccionar a cambios en la vida del jugador,
        // puedes hacerlo en otros m�todos espec�ficos o scripts.
    }
}
