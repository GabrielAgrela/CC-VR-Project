# CG-VR-Project
Complicated Grief Virtual Reality Therapy University Project 

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
Scenes
-Launcher
  -LauncherGameManager
    -Ao clicar no botão de um modelo, guardá-lo para a instanciá-lo na proxima scene (Room). Depois segue-se o input do username.
    -Ao receber o username e ao carregar enter/desfocar-se do input holder, connecta-se aos servers da Photon criando um lobby (no caso de ter selecionado o modelo do policia) ou entrando no lobby criado (no caso de qualquer outro modelo selecionado).
  -Canvas
    -ChooseChar
      -Possui os botões, placeholders e modelos rotativos dos utilizadores.
      -Ao clicar num botão de um modelo, ativa o EnterUsername.
    -EnterUsername
      -Recebe um username e ativa o Loading.
    -Loading
      -Placeholder de loading e animação do mesmo.

