# Clone_Survivor.io

> 탕탕특공대 모작 <br/>
> 기간 : 2025.01 ~ 2025.02 <br/>
> Unity2D <br/>

<img src="https://github.com/user-attachments/assets/4e225a86-47ca-45ab-bc11-13bd53c2ee26" width=400> <br/>
<img src="https://github.com/user-attachments/assets/d12fc5d8-b4e2-4dc4-9608-e0af1de893ae" width=600> <br/>

<br/>

## Manager Class
- 앱의 라이프 사이클 전반에 필요한 기능들을 메니저 클레스로 설정해서 특정 씬에 할당
- 다른 씬의 로드, 언로드 시에도 Destroy 되지 않고 남도록 씬 이동 로직을 구성

<br/>

## Object Pooling
- 오브젝트의 생성과 파괴가 잦지만 같은 오브젝트를 많이 사용하는 게임 특성상, 생성한 오브젝트를 파괴하지 않고 오브젝트 풀링 기법으로 재활용해서 사용

<br/>

## Lobby
- 설정 변경, 장비 변경 등 다른 기능을 사용하기 위한 로비

<img src="https://github.com/user-attachments/assets/b063bd33-3ee9-4e2e-965d-60677e46fc99" width=200> <br/>

<br/>

## Chapter
- 메인 컨텐츠
- 15분 동안 진행되는 스테이지를 HP가 0이 되지 않고 버티면 클리어
- 진행하는 동안, 시간의 흐름에 따라 다양한 이벤트 발생

<br/>

## Character
- 쳅터 등의 컨텐츠 진입 시 플레이어가 컨트롤하여 게임을 플레이하는 매개체
- 플레이 시 필요한 스테이터스, 충돌 판정, Skill 등록 등의 기능을 수행

<br/>

## Skill
- 스킬 획득 조건을 만족하면 획득할 수 있는 공격, 유틸리티 스킬
1. ActiveSkill
  > 보유 시, 자동으로 공격하는 Skill <br/>
  > 캐릭터의 스테이터스가 성능에 반영
2. PassiveSkill
  > 보유 시, 캐릭터의 스테이터스를 강화하는 Skill
- 각 Skill은 업그레이드 시 효과가 강화

<img src="https://github.com/user-attachments/assets/7655f27c-d0f8-41d0-b8cf-6fc0877006f1" width=200> <br/>

<br/>

## Monster
- 캐릭터를 적대하는 오브젝트
- Moster 형을 상속받는 다양한 몬스터가 있다
- interface를 사용해서 Skill과 상호작용하여, 종류에 구애받지 않고 필요한 메소드만 실행시키도록 설계

<p align="left">
  <img src="https://github.com/user-attachments/assets/0ab0b955-e6ef-40fc-b2df-ebefaf859048" alt="Image 1" width="200" style="display:inline-block;">
  <img src="https://github.com/user-attachments/assets/a96f420b-6e69-4881-ab6a-0b84211855bc" alt="Image 2" width="200" style="display:inline-block;">
  <img src="https://github.com/user-attachments/assets/2dfe9036-c666-45ce-b80d-dbde0228e555" alt="Image 3" width="200" style="display:inline-block;">
</p>
