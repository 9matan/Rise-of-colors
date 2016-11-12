using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using RoC;


public class UI : MonoBehaviour
{
  public Text red_score;
  public Text green_score;
  public Text blue_score;

  void Start()
  {
    RoCColorScore.updatescore+=updateScore;
  }

  void Update()
  {
  }

  private void updateScore( ERoCColor color, int value )
  {
    switch ( color )
    {
    case ERoCColor.NONE:
      break;
    case ERoCColor.RED:
      red_score.text = value.ToString();
      break;
    case ERoCColor.GREEN:
      green_score.text = value.ToString();
      break;
    case ERoCColor.BLUE:
      blue_score.text = value.ToString();
      break;
    case ERoCColor.YELLOW:
      break;
    case ERoCColor.WHITE:
      break;
    case ERoCColor.BLACK:
      break;
    default:
      break;
    }

  }
}
