# VR_ExperienceCenter
## ⌨️개발 내용

### 1.Unity Interaction Toolkit을 활용한 interaction구현 (grab, socket, raycast, inputaction, haptic, 등)
- XR Grab Interactable을 활용하여 착시 효과를 적용할 오브젝트 grab, 컨트롤러는 RayInteractor 사용 
- XR Socket Interactor 사용하여 오브젝트를 정해진 곳에 태그하면 문이 열리도록 하였음
- 플레이어 컨트롤러 raycast외에도 maincamera 중앙에서 raycast를 생성하여 grab한 오브젝트가 grab이 풀렸을 때 이동할 위치를 가져옴
- 플레이어 소리가 일정 크기 이상 들어오면 컨트롤러에 haptic반응이 오도록 함

  
### 2. 같은 크기의 물체가 거리에 따라 크기가 다르게 보이는 것을 활용하여 착시 구현(ForcePerspectiveEffect)
- 오브젝트를 처음 grab했을 때 카메라와 오브젝트의 거리를 오브젝트가 grab이 풀려 이동한 후 거리와 비교하여 증가 또는 감소한 비율에 따라 스케일을 조정
- 오브젝트의 거리가 4배 증가하면 스케일은 2배 증가, 플레이어가 화면으로 보는 오브젝트의 상대적 크기가 실제로 적용됨
![image](https://github.com/poiz1245/VR_ExperienceCenter/assets/139199211/0f14bfdb-4a36-436e-8b36-f40395ab1875)


### 3. 착시 그림을 오브젝트화 하는 기능 구현(AnamorphicEffect)
- 착시의 일종인 Anamorphic효과를 일으키는 그림을 오브젝트에 텍스쳐로 입혀서 플레이어가 특정 지점에서 특정 각도로 바라보면 오브젝트가 실제화 되도록 구현
![image](https://github.com/poiz1245/VR_ExperienceCenter/assets/139199211/32a65024-419d-40f2-a733-2aa96fc2b5e0)

### 4. Stecil을 활용한 포탈 효과 구현
- urp render setting을 이용하여 stencil효과 구현
- stencil shader 작성
- 문을 통해서만 다음 공간이 보이도록 하고 문을 통과하면 실체화 하도록 구현

### 5. Shader를 사용하여 착시효과 보완 (grab한 오브젝트는 가장 앞에 렌더링)
- shader의 ztest와 zwirte를 변경하여 grab한 오브젝트는 항상 가장 앞에 렌더링 되도록 함
- 오브젝트와 플레이어의 거리는 항상 일정하기 때문에 실제로 오브젝트가 다른 오브젝트의 뒤로 넘어가더라도 더 앞에 있는것 같은 착시를 일으킴
  ![image](https://github.com/poiz1245/VR_ExperienceCenter/assets/139199211/9736dee8-476f-4c3a-88bc-aef9ba5687cf)
- 오브젝트 grab이 풀리면 다시 원래 material로 변경

  
### 6. 플레이어 마이크의 소리에 따라 오브젝트의 크기를 변화시키는 기능 구현
- 플레이어 마이크 소리를 float값으로 변환하여 사용
- 소리의 크기에 따라서 오브젝트의 크기를 변화시키거나 UI slider의 value를 변화시켜 소리 크기를 시각화


### 7.  Mesh 자르는 기능 구현(EZslice 라이브러리 사용)
- EZ Slice활용하여 오브젝트 메쉬 자르는 기능 구현
- 자른 오브젝트 착시효과 적용하여 크기 확대 축소 가능

  
### 8. Animation Rigging, 캐릭터를 HMD, Controller와 동기화
- Animation Rigging 적용하여 캐릭터가 플레이어 VR Controller와 HMD와 동기화 되어 움직이도록 함
 
