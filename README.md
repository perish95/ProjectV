# ProjectV

Unity로 개발 중인 뱀서라이크(로그라이크 서바이벌) 게임 프로젝트입니다.
플레이어가 몰려오는 적을 처치하며 경험치를 모아 성장하고, 페이즈(웨이브)가 진행되며 난이도가 올라가는 구조를 목표로 합니다.

## 개발 환경

- **엔진**: Unity 2022.3.58f1 (LTS)
- **렌더 파이프라인**: Universal Render Pipeline (URP) 14.0.11
- **주요 패키지**: TextMesh Pro, Timeline, Visual Scripting, Unity Test Framework

## 프로젝트 구조

```
Assets/
├─ Scripts/
│  ├─ Player/     # 플레이어 컨트롤 및 추적 카메라
│  ├─ Enemy/      # 적 스폰, 근접/원거리 적, 스탯
│  ├─ Item/       # 무기/패시브 아이템, 인벤토리
│  ├─ Phase/       # 페이즈(웨이브) 상태 머신
│  ├─ UI/         # 타이머 등 UI 로직
│  ├─ ExpOrb.cs           # 경험치 오브젝트
│  ├─ ObjectPoolManager.cs # 오브젝트 풀링
│  └─ GameSingleton.cs / SceneSingleton.cs # 싱글톤 유틸
├─ Scenes/        # 게임 씬
├─ Materials/, Sprites/, Perfabs/  # 아트/프리팹 리소스
└─ Downloads/     # 외부(에셋 스토어 등) 리소스
```

## 라이선스

별도 명시 전까지 이 저장소는 비공개/개인 프로젝트 용도로 사용합니다.
