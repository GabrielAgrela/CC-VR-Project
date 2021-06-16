# CG-VR-Project
Complicated Grief Virtual Reality Therapy Universidade da Madeira Project 

# Motivation
Grief is a strong emotion typically experienced after the loss of a loved one. It is common to experience sadness and hopelessness, and these feelings of loss are often resolved over time. However, some people do not improve over time sustaining strong feelings of loss that have a negative impact on their lives. This is a disorder known as complicated grief.
This project aims at developing a VR-based intervention prototype to support the management of grief.

# Main goals
• Do a literature review on ICT based solutions for grief;  
• Ideate a VR prototype to address the management of grief;  
• Use the Unity 3D game engine for the design and development of the prototype;  
• Document all the process and deliver all project files;  
• Do a presentation of the conducted work.  

# Documenting the project technicalities
- Launcher Scene
  - LauncherGameManager
    - Ao clicar no botão de um modelo, guardá-lo para a instanciá-lo na proxima scene (Room). Depois segue-se o input do username.
    - Ao receber o username e ao carregar enter/desfocar-se do input holder, connecta-se aos servers da Photon criando um lobby (no caso de ter selecionado o modelo do policia) ou entrando no lobby criado (no caso de qualquer outro modelo selecionado).
  - Canvas
    - ChooseChar
      - Possui os botões, placeholders e modelos rotativos dos utilizadores.
      - Ao clicar num botão de um modelo, ativa o EnterUsername.
    - EnterUsername
      - Recebe um username e ativa o Loading.
    - Loading
      - Placeholder de loading e animação do mesmo.
  - Camera
    - A camera é o tipo de camera normal esperado num jogo não VR. Isto porque o launcher deve ser executado no computador sem o uso do headset, de modo a introduzir os dados do utilizador.



- Room Scene
  - RoomGameManager
    - Kick
      - Recebe o input em string do user a kickar, procura-o recebendo a lista de jogadores na Room em pares valores-chave e cancela a conexão com os servers da Photon.
    - Awake
      - Se o cliente for o moderador, Instancia por network o mapa inicial (neste caso o DesertMap) e ativa o Canvas localmente.
      - Caso se trate de um utilizador normal, inicia a scene em versão VR.
      - Para ambos instancia por network o modelo de utilizador escolhido na scene anterior.
    - Spawn (generico)
      - No caso de um mapa
        - Dependendo do botão escolhido pelo moderador, notifica os utilizadores, através do método RPC changingMapFlag() que o mapa está a ser trocado.
      - Destrói por network o elemento anterior do mesmo tipo (por exemplo: no caso de instanciar um mapa, destrói o mapa anterior).
      - Dependendo do botão escolhido instancia o objeto po network.
    - RPC (generico)
      - Ao ser chamado anteriormente, corre este método em todos os clientes (não moderadores) que o possuem, de modo a saberem que devem cobrir (ou não) os olhos com o objeto preto.
  - Canvas
    - Maps/Weather/Melodies
      - Botões para cada um dos tipos de objeto a instanciar, definidos pelo parâmetro e tratados no switch-case dos métodos de spawn no RoomGameManager. 
    - Kick
      - Placeholder recebe o username do utilizador a kickar, executado ao carregar enter ou perder focus.
  - CanvasKicked
    - Ao ser removido/kickado da Room, o modelo do utilizador desaparece, ao ser null, ativa este canvas.
  - Map/Models/Elements (generico)
    - Objetos instanciado por networking pelos botões.


- Player's Objects
  - Modelo (generico)
    - DefaultPlayerBody
      - Mesh renderer/ Steam VR_Play Area
        - Scripts relacionados com o espaço na vida real em que o user se pode movimentar.
      - RigidBody
        - Usado sobretudo para proporcionar gravidade ao objeto. Desativado no caso de um moderador.
      - Capsule collider
        - Hitbox do modelo, em forma de cápsula. Desativado no caso de um moderado.
      - PhotonView e Photon Transform View Classic
        - Scripts da library Photon que possibilitam o movimento dos objetos em networking, assim como a configuração do tipo de interpolação, extrapolação, etc.
      - PlayerScript
        - Start
          - Unrender o modelo do próprio utilizador, de modo a não bloquear a própria camera.
          - No caso do utilizador ser o moderador, define o maximo de fps e desativa a sua gravidade e collider.    
          - Cap fps
          - No caso de ser o moderador, transforma o fps do cliente em 60.
        - un/Block Vision
          - No caso de ser um utilizador normal, e a variável manipulada pelo metodo RPC no RoomGameManager “changingMap” ser true, ativa o cubo e bloqueia a visão do utilizador.
          - O metodo rpc será chamado outra vez e changingMap será false depois de 5s.
        - Match model with Headset rotation & Y position
          - Iguala o valor da rotação no eixo Y do modelo do utilizador com o da camera (de modo a rodar ao mesmo tempo que o utilizador na vida real) e aumenta o valor da posição da camera uma unidade no eixo do Y de modo a ficar a altura da cabeça do modelo do utilizador.
        - Movement
          - No caso do moderador, pode utilizar o shift e control para subir e descer, respectivamente, e QE para rodar no eixo do Y.
          - Para todos os utilizadores, movimenta-se com o WASD.
          - Ao pressionar W inicia a animação, por networking, de movimento.
      - SpecificPlayerBody
        - Renderers
          - Modelo específico deste objeto (redwoman, policeman,etc).
        - Photon scripts e animations
          - Componentes relacionados com as animações e networking das mesmas.
        - Audio
          - Componentes relacionados o chat por voz da Photon (receção e transmissão),
        - SetMultiplayerCamera
          - Ativa a camera do objeto certo (de modo a não ter o utilizador X a utilizar o POV do utilizador Y).
      - Highlighter
        - Canvas 
          - Estarão aqui os elementos UI dos seus filhos (Username, icone de voz)
        - Speaker
          - Icone de voz que é ativado quando é detetado som no microfone do cliente.
          - Username
            - UI text definido pelo SetHIghlighterText
            - SetHighlighterText
              - Define o texto no UI do canvas anterior dependendo do nome do utilizador do modelo em questão.
    - Camera
      - Steam VR_Camera Helper/Universal additional camera/Tracked pose driver.
        - Scripts da library da SteamVR, definem a deteção do headset e movimento da camera.
      - Cubo
        - Ativado/Desativado com o método RPC proveniente do RoomGameLauncher de modo a cobrir os olhos do utilizador quando mudar de mapa, pois o mesmo é teleportado para 20 unidades acima no eixo Y de modo a ser possível transacionar de um mapa para outro. Isto diminui o risco de náuseas.
