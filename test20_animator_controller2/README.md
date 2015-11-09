memo
====

�\��
----

    Click�܂��̓{�^�� �� CubeController.Fire()   �� Cube1��AnimatorAnimator.SetTrigger("fire")
                          ��
                         SphereController.Fire() �� Sphere��Animator.SetTrigger("fire")
                          ��(1�b��)
                         CubeController.Fire()   �� Cube2��AnimatorAnimator.SetTrigger("fire")


click�C�x���g�̎���
----
1. �V�[����EventSystem��ǉ�����
  - GameObject �� UI �� Event System

2. ���C���J������Physics Raycaster���A�^�b�`����B

3. IPointerClickHandler�����������N���X���쐬�BGameObject�ɃA�^�b�`����B

    public class Click : MonoBehaviour, IPointerClickHandler
    {
        public CubeAnimation cube;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (cube == null) return;
            cube.Fire();
        }
    }

