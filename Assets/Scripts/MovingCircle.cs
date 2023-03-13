
using UnityEngine;



public class MovingCircle : MonoBehaviour
{
    private int _frames;
    private bool _isEnable;
    private void Update()
    {
        if (!_isEnable) return;
        if (_frames == 150)
        {
            var randomAngle = Random.Range(-30,30);
            var objTransform = transform;
            objTransform.eulerAngles = new Vector3(0, 0, randomAngle + objTransform.eulerAngles.z);
            _frames = 0;
            
        }
        
        var hitCentre= Physics2D.Raycast(transform.position + transform.right * transform.localScale.x/2,transform.right,0.5f);
        var hitRight = Physics2D.Raycast(transform.position + transform.up * transform.localScale.x/2,transform.right,0.3f);
        var hitLeft = Physics2D.Raycast(transform.position + transform.up * -transform.localScale.x/2,transform.right,0.3f);
        
        if (hitCentre.point != Vector2.zero || hitRight.point != Vector2.zero || hitLeft.point != Vector2.zero);
        {
            var counter = 0;
            while (hitCentre.point != Vector2.zero || hitRight.point != Vector2.zero || hitLeft.point != Vector2.zero)
            {
                counter++;
                var angle = Random.Range(-180, 180);
                transform.eulerAngles = new Vector3(0, 0, angle + transform.eulerAngles.z);
                hitCentre = Physics2D.Raycast(transform.position + transform.right * transform.localScale.x/2, transform.right, 0.5f);
                hitRight = Physics2D.Raycast(transform.position + transform.up * transform.localScale.x/2,transform.right,0.3f);
                hitLeft = Physics2D.Raycast(transform.position + transform.up * -transform.localScale.x/2,transform.right,0.3f);
                if (counter > 100)
                {
                    Destroy(gameObject);
                }
            }

        }

       
        transform.position = Vector2.MoveTowards(transform.position, (transform.position+transform.right), 1.5f * Time.deltaTime);
        
        _frames++;
    }

    public void TurnOn()
    {
        _isEnable = true;
    }
}
