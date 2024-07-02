# VR_ExperienceCenter
## ⌨️개발 내용

### 1.Unity Interaction Toolkit을 활용한 interaction구현 (grab, socket, raycast, inputaction, haptic, 등)
- XR Grab Interactable을 활용하여 착시 효과를 적용할 오브젝트 grab, 컨트롤러는 RayInteractor 사용 
- XR Socket Interactor 사용하여 오브젝트를 정해진 곳에 태그하면 문이 열리도록 하였음
- 플레이어 컨트롤러 raycast외에도 maincamera 중앙에서 raycast를 생성하여 grab한 오브젝트가 grab이 풀렸을 때 이동할 위치를 가져옴
- 플레이어 소리가 일정 크기 이상 들어오면 컨트롤러에 haptic반응이 오도록 함

  
### 2. 같은 크기의 물체가 거리에 따라 크기가 다르게 보이는 것을 활용하여 착시 구현(ForcePerspectiveEffect)
- 오브젝트를 처음 grab했을 때 카메라와 오브젝트의 거리를 오브젝트가 grab이 풀려 이동한 후 거리와 비교하여 증가 또는 감소한 비율에 따라 스케일을 조정
![image](https://github.com/poiz1245/VR_ExperienceCenter/assets/139199211/0f14bfdb-4a36-436e-8b36-f40395ab1875)


### 3. 착시 그림을 오브젝트화 하는 기능 구현(AnamorphicEffect)

### 4. Stecil을 활용한 포탈 효과 구현


### 5. Shader를 사용하여 착시효과 보완 (grab한 오브젝트는 가장 앞에 렌더링)

  
### 6. 플레이어 마이크의 소리에 따라 오브젝트의 크기를 변화시키는 기능 구현
