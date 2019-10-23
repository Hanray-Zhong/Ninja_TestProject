using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

namespace KanetoTools
{
    public class CameraFollow : MonoBehaviour
    {
        public bool FollowMouse;
        public Transform player;//获得角色
        public Vector2 Margin;//相机与角色的相对范围
        public Vector2 smoothing;//相机移动的平滑度
        public BoxCollider2D Bounds;//背景的边界
        [Header("Extra")]
        public bool PlayerToMouse;
        public bool MouseToPlayer;
 
        private Vector3 _min;
        private Vector3 _max;
 
        public bool IsFollowing;//用来判断是否跟随
    
        void Start(){
            UpdateBounds();
        }
 
        void Update(){
            if (player == null && !FollowMouse) {
                return;
            }
            Vector3 pos;
            if (!FollowMouse) {
                pos = player.position;
            }
            else {
                pos = KanetoTools.World_Screen_Translation.ScreenToWorld(gameObject, Input.mousePosition);
            }
            var x = transform.position.x;
            var y = transform.position.y;
            if (IsFollowing) {
                if (Mathf.Abs(x - pos.x) > Margin.x) {//如果相机与角色的x轴距离超过了最大范围则将x平滑的移动到目标点的x
                    x = Mathf.Lerp(x, pos.x, smoothing.x * Time.deltaTime);
                }
                if (Mathf.Abs (y - pos.y)> Margin.y) {//如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的y
                    y = Mathf.Lerp(y, pos.y, smoothing.y * Time.deltaTime);
                }
            }
            float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
            var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
            x = Mathf.Clamp (x, _min.x + cameraHalfWidth, _max.x-cameraHalfWidth);//限定x值
            y = Mathf.Clamp (y, _min.y + orthographicSize, _max.y-orthographicSize);//限定y值
            transform.position = new Vector3(x, y, transform.position.z);//改变相机的位置
            Extra();
        }

        public void UpdateBounds() {
            if (Bounds != null) {
                _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
                _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
                IsFollowing = true;//默认为跟随
            }
        }

        public void Extra() {
            if (PlayerToMouse) {
                if (Vector2.Distance(player.transform.position, gameObject.transform.position) < 0.5) {
                    FollowMouse = true;
                    Margin = new Vector2(8, 4);
                    PlayerToMouse = false;
                }
            }
            if (MouseToPlayer) {
                
            }
        }
    }
}

