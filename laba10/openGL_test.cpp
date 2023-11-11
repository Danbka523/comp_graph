#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include<iostream>


void SetIcon(sf::Window& wnd);
void Init();
void Draw();
void Release();
void InitVBO();
void InitShader();
void checkOpenGLerror();
void ReleaseVBO();
void ReleaseShader();
void ShaderLog(unsigned int shader);

struct Vertex {
    GLfloat x;
    GLfloat y;
};

GLuint Program;
GLuint Attrib_vertex;
GLuint VBO;

// Исходный код вершинного шейдера
const char* VertexShaderSource = R"(
    #version 330 core
    in vec2 coord;
    void main() {
        
        gl_Position = vec4(coord.y*0.2,coord.x*0.8, 1.0, 1.0);

    }
)";

// Исходный код фрагментного шейдера
const char* FragShaderSource = R"(
    #version 330 core
    out vec4 color;
    void main() {
        color = vec4(1, 0.5, 0, 1);
    }
)";


int main() {
    // Создаём окно
    sf::Window window(sf::VideoMode(600, 600), "laba10", sf::Style::Default, sf::ContextSettings(32));
    // Ставим иконку (окна с дефолтной картинкой это некрасиво)
    SetIcon(window);
    // Включаем вертикальную синхронизацию (синхронизация частоты отрисовки с частотой кадров монитора, чтобы картинка не фризила, делать это не обязательно)
    window.setVerticalSyncEnabled(true);

    // Активируем окно
    window.setActive(true);

    // Инициализация
    glewInit();
    Init();
    // Главный цикл окна
    while (window.isOpen()) {
        
        sf::Event event;
        // Цикл обработки событий окна, тут можно ловить нажатия клавиш, движения мышки и всякое такое
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed) {
                // Окно закрыто, выходим из цикла
                window.close();
            }
            else if (event.type == sf::Event::Resized) {
                // Изменён размер окна, надо поменять и размер области Opengl отрисовки
                glViewport(0, 0, event.size.width, event.size.height);
            }
        }

        // Очистка буферов
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // Рисуем сцену
        Draw();

        // Отрисовывает кадр - меняет передний и задний буфер местами
        window.display();
    }
    Release();
    return 0;
}


// В момент инициализации разумно произвести загрузку текстур, моделей и других вещей
void Release() {
    // Шейдеры
    ReleaseShader();
    // Вершинный буфер
    ReleaseVBO();

}
void ReleaseVBO() {
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glDeleteBuffers(1, &VBO);
}

void ReleaseShader() {
    // Передавая ноль, мы отключаем шейдерную программу
    glUseProgram(0);
    // Удаляем шейдерную программу
    glDeleteProgram(Program);
}

void Init() {
    InitShader();
    InitVBO();
}

void InitShader() {
    // Создаем вершинный шейдер
    GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
    // Передаем исходный код
    glShaderSource(vShader, 1, &VertexShaderSource, NULL);
    // Компилируем шейдер
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    // Функция печати лога шейдера
    ShaderLog(vShader);
        // Создаем фрагментный шейдер
        GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    // Передаем исходный код
    glShaderSource(fShader, 1, &FragShaderSource, NULL);
    // Компилируем шейдер
    glCompileShader(fShader);
    std::cout << "fragment shader \n";
    // Функция печати лога шейдера
    ShaderLog(fShader);
        // Создаем программу и прикрепляем шейдеры к ней
        Program = glCreateProgram();
    glAttachShader(Program, vShader);
    glAttachShader(Program, fShader);
    // Линкуем шейдерную программу
    glLinkProgram(Program);
    // Проверяем статус сборки
    int link_ok;
    glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
    if (!link_ok) {
        std::cout << "error attach shaders \n";
        return;
    }
        // Вытягиваем ID атрибута из собранной программы
        const char* attr_name = "coord"; //имя в шейдере
    Attrib_vertex = glGetAttribLocation(Program, attr_name);
    if (Attrib_vertex == -1) {
        std::cout << "could not bind attrib " << attr_name << std::endl;
        return;
    }
    checkOpenGLerror();
}


void InitVBO() {
    glGenBuffers(1, &VBO); //glew.h
    Vertex triangle[3] = {
        {-1.0f,-1.0f},
        {0.0f,1.0f},
        {1.0f,-1.0f}
    };

    glBindBuffer(GL_ARRAY_BUFFER, VBO);
    glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
    checkOpenGLerror();
}

void ShaderLog(unsigned int shader)
{
    int infologLen = 0;
    glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infologLen);
    if (infologLen > 1)
    {
        int charsWritten = 0;
        std::vector<char> infoLog(infologLen);
        glGetShaderInfoLog(shader, infologLen, &charsWritten, infoLog.data());
        std::cout << "InfoLog: " << infoLog.data() << std::endl;
    }
}

// Функция непосредственно отрисовки сцены
void Draw() {
    glUseProgram(Program); // Устанавливаем шейдерную программу текущей

    glEnableVertexAttribArray(Attrib_vertex); // Включаем массив атрибутов
    glBindBuffer(GL_ARRAY_BUFFER, VBO); // Подключаем VBO
    // Указывая pointer 0 при подключенном буфере, мы указываем что данные в VBO
    glVertexAttribPointer(Attrib_vertex, 2, GL_FLOAT, GL_FALSE, 0, 0);
    glBindBuffer(GL_ARRAY_BUFFER, 0); // Отключаем VBO
    glDrawArrays(GL_TRIANGLES, 0, 3); // Передаем данные на видеокарту(рисуем)
    glDisableVertexAttribArray(Attrib_vertex); // Отключаем массив атрибутов
    glUseProgram(0); // Отключаем шейдерную программу
    checkOpenGLerror();
}


void SetIcon(sf::Window& wnd)
{
    sf::Image image;

    // Вместо рисования пикселей, можно загрузить иконку из файла (image.loadFromFile("icon.png"))
    image.create(16, 16);
    for (int i = 0; i < 16; ++i)
        for (int j = 0; j < 16; ++j)
            image.setPixel(i, j, {
                (sf::Uint8)(i * 16), (sf::Uint8)(j * 16), 0 });

    wnd.setIcon(image.getSize().x, image.getSize().y, image.getPixelsPtr());
}

void checkOpenGLerror() {
    GLenum err;
    while ((err = glGetError()) != GL_NO_ERROR)
    {
      //  printf("smth went wrong");
    }
}