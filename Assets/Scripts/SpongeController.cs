using UnityEngine;

public class SpongeController : MonoBehaviour
{
    [SerializeField] private Vector3 _spongeOffset;

    private Vector3 _mousePosition;
    private float _posZ;
    private Material[] _spongeMaterials;
    private Color _currentMixedColor;
    private Color _lastMixedColor;
    private bool canMix = true;

    private void Start()
    {
        _spongeMaterials = GetComponent<MeshRenderer>().materials;
    }

    private void Update()
    {
        #region Camera Raycast + MouseMoving
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Позиция(перемещение) губки относительно ведер краски и логика перекрашивания губки
            if (hitInfo.collider.tag == "PaintBucket")
            {
                //Перемещение губки
                Vector3 bucketWorldPos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z - 2);
                transform.position = bucketWorldPos;

                //Материалы ведра на которого навелись губкой
                Material[] clickedBucketMaterial = hitInfo.collider.GetComponent<MeshRenderer>().materials;

                //Замена материала губки при нажатии на ведро краски
                if (Input.GetMouseButtonDown(0))
                {
                    //Берем все материалы ведра и передаем в метод SpongeColoration
                    SpongeColoration(clickedBucketMaterial);
                    
                }

                //Замена материала губки при проведение с ведром краски              
                if (canMix)
                {
                    SpongeColorMixing(clickedBucketMaterial);
                } 
                
                return;
            }

            //Позиция(перемещение) губки относительно точки соприкосновение луча
            else
            {
                //Из точки луча вычитаем смещение
                Vector3 worldPos = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z) - _spongeOffset;

                transform.position = worldPos;
            }
        }
        #endregion

        

    }

    //Смена цвета губки при клике на ведро
    private void SpongeColoration(Material[] bucketMaterial)
    {
        //Присваиваем губке последний материал ведра
        _spongeMaterials[1].color = bucketMaterial[bucketMaterial.Length - 1].color;       
    }


    //Смена цвета губки при наведении на ведро(смешивание красок)
    private void SpongeColorMixing(Material[] bucketMaterial)
    {
        //Запоминаем текущий цвет ведра краски
        _currentMixedColor = bucketMaterial[bucketMaterial.Length - 1].color;

        //Если цвет губки не равен текущему цвету ведра 
        //и текущий цвет ведра не равен последнему цвету ведра, тогда перекрашиваем
        if (_spongeMaterials[1].color != _currentMixedColor && 
            _currentMixedColor != _lastMixedColor)
        {
            //Перекраска - цвет губки в текущий цвет ведра
            _spongeMaterials[1].color *= _currentMixedColor;

            //Текущий цвет ведра сохраняем в переменную последнего цвета для того чтобы не красить снова в текущий цвет
            _lastMixedColor = _currentMixedColor; 
        }
    }
}
