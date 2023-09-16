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
    public int startingLife = 10; // Puedes ajustar esto según tus necesidades.
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
                // Llama a un método para reiniciar la escena.
                RestartScene();
            }
        }
    }
    private void RestartScene()
    {
        // Obtén el índice de la escena actual.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Carga la escena actual para reiniciarla.
        SceneManager.LoadScene(currentSceneIndex);
    }

    // Lista de observadores (listeners) que se notificarán cuando cambie la vida.
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

    // Método para recibir daño.
    public void TakeDamage(int damageAmount)
    {
        Life -= damageAmount;

        // Puedes agregar lógica adicional aquí, como verificar si el jugador murió.
    }

    // Método para obtener curación.
    public void Heal(int healAmount)
    {
        Life += healAmount;
    }

    public void OnLifeChanged(int newLife)
    {
        // Este método se llama cuando cambia la vida del jugador,
        // pero no necesitas hacer nada específico aquí para el jugador.
        // Si deseas reaccionar a cambios en la vida del jugador,
        // puedes hacerlo en otros métodos específicos o scripts.
    }
}
