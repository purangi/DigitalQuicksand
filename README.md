# DigitalQuicksand
서울여자대학교 졸업 프로젝트

![thesis_poster](https://github.com/purangi/DigitalQuicksand/assets/68212670/a9a39d3a-07a7-46ba-8b93-05fef77948ad)

- 한국인터넷정보학회 2023 추계 학술 대회 포스터 발표
- 한국저작권위원회 컴퓨터프로그램저작물 등록 (등록 번호 : C-2023-059539)

### Play
🔗 : [빌드 파일 ](https://drive.google.com/file/d/1e97AvOD1DtFlOJ9NIJYZvCWdP_RMoGs3/view?usp=drive_link)

[~웹 빌드 링크~](https://play.unity.com/mg/other/webgl-builds-405746) → dll not found 오류남

### ERD 다이어그램
![image](https://github.com/purangi/DigitalQuicksand/assets/68212670/7f4b6858-27ee-4b54-9cce-9939a22fe3df)


### 기여도 (김유원)
- **개발 전반 담당**
- 데이터 자료 구조 설계 및 구현
- SqliteDB를 내부 DB로 연동
- 핵심 기능인 키워드 기반 영상 추천 알고리즘 구현 [https://github.com/purangi/DigitalQuicksand/blob/main/DigitalQuicksand/Assets/Script/VideoList.cs](https://github.com/purangi/DigitalQuicksand/blob/main/Assets/Script/Video/VideoList.cs)
  - 소장르 분류 중 가장 흥미도가 높은 순으로 분류하고 같은 흥미도에 대해 시청 횟수 순으로 분류하여 최소 소장르 개수가 5개가 되는 시점의 흥미도와 시청 횟수 파악
  - 앞서 파악했던 흥미도`interest`와 시청 횟수`count`의 최솟값을 기준으로 우선도 높은 소장르 5개 뽑기
  - 앞서 뽑은 5개의 소장르에 해당하는 영상들을 검색하여 15개 랜덤 선택, 영상 리스트 제공
