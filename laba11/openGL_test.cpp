#define _USE_MATH_DEFINES
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <cmath>
#include <iostream>



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

bool is_gradient=1;
bool is_uni;
bool is_const;
bool is_fan;
bool is_quad;
bool is_triag;
bool is_penta = 1;

struct Vertex {
    GLfloat x;
    GLfloat y;
};

GLuint Program;
GLuint Attrib_vertex;
GLuint Attrib_color;
GLuint VBO;
GLuint VBO_color;
GLuint location;

// Исходный код вершинного шейдера
const char* VertexShaderSource = R"(
    #version 330 core
    in vec2 coord;
    void main() {
        
        gl_Position = vec4(coord,1.0, 1.0);

    }
)";

const char* VertexShaderGradientSource = R"(
    #version 330 core
    in vec2 coord;
    in vec4 color;
    out vec4 vertexColor;
    void main() {
        gl_Position = vec4(coord,1.0, 1.0);
        vertexColor = color;
    }
)";


// Исходный код фрагментного шейдера
//
const char* FragShaderSourceConstColor = R"(
    #version 330 core
    out vec4 color;
    const vec3 c=vec3(0,0,1);
    void main() {
        color = vec4(c, 1);
    }
)";


const char* FragShaderSourceGradientColor = R"(
    #version 330 core
    in vec4 vertexColor;
    out vec4 color;
    void main() {
        color = vertexColor;
    }
)";

const char* FragShaderSourceUniformColor = R"(
    #version 330 core
    uniform vec4 color;
    void main() {
        gl_FragColor = color;
    }
)";

int main() {
    // Создаём окно
    sf::Window window(sf::VideoMode(600, 600), "laba11", sf::Style::Default, sf::ContextSettings(32));
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
    if (!is_gradient)
        glShaderSource(vShader, 1, &VertexShaderSource, NULL);
    else
    //gradient
        glShaderSource(vShader, 1, &VertexShaderGradientSource, NULL);
    // Компилируем шейдер
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    // Функция печати лога шейдера
    ShaderLog(vShader);
        // Создаем фрагментный шейдер
        GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    // Передаем исходный код
    
    //для плоской заливки констатнотой в шейдере
    if (is_const)
        glShaderSource(fShader, 1, &FragShaderSourceConstColor, NULL);

    if (is_uni)
    //uniform
        glShaderSource(fShader, 1, &FragShaderSourceUniformColor, NULL); 
    
    if (is_gradient)
    //градиент
        glShaderSource(fShader, 1, &FragShaderSourceGradientColor, NULL);




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
        
            if (is_gradient) {
                const char* attr_name1 = "color"; //имя в шейдере
                Attrib_color = glGetAttribLocation(Program, attr_name1);
                if (Attrib_color == -1) {
                    std::cout << "could not bind color " << attr_name << std::endl;
                    return;
                }
            }
    checkOpenGLerror();
}


void InitVBO() {
    glGenBuffers(1, &VBO); //glew.h
    glGenBuffers(1, &VBO_color);
    Vertex triangle[3] = {
        {-1.0f,-1.0f},
        {0.0f,1.0f},
        {1.0f,-1.0f}
    };

    Vertex quadrilateral[4] = {
        {-0.5f,-0.5f},
        {-0.5f,0.5f},
        {0.5f,0.5f},
        {0.5f,-0.5f}
    };

    float radius = 1.f;
    float angleinc = 360.f / 5.0f;
    angleinc *= M_PI / 180.0f;
    float angle = 0.0f;
    Vertex pentagon[5] = {
        {radius * cos(angle + angleinc * 1),radius * sin(angle + angleinc * 1)},
        {radius * cos(angle + angleinc * 2),radius * sin(angle + angleinc * 2)},
        {radius * cos(angle + angleinc * 3),radius * sin(angle + angleinc * 3)},
        {radius * cos(angle + angleinc * 4),radius * sin(angle + angleinc * 4)},
        {radius * cos(angle + angleinc * 5),radius * sin(angle + angleinc * 5)},
    };

    float phi = M_PI * 2 / 27;

    Vertex fan[]{
        {0,0},
        {cos(M_PI/9),sin(M_PI/9)},
        {cos(2*M_PI/9),sin(2*M_PI/9)},
        {cos(3*M_PI/9),sin(3*M_PI/9)},
        {cos(4*M_PI/9),sin(4*M_PI/9)},
        {cos(5*M_PI/9),sin(5*M_PI/9)},
        {cos(6*M_PI/9),sin(6*M_PI/9)},
        {cos(7*M_PI/9),sin(7*M_PI/9)},
        {cos(8*M_PI/9),sin(8*M_PI/9)},
    };



    glBindBuffer(GL_ARRAY_BUFFER, VBO);

    //Треугольник
    if (is_triag)
        glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
    
    //Четырехугольник
    if (is_quad)
        glBufferData(GL_ARRAY_BUFFER, sizeof(quadrilateral), quadrilateral, GL_STATIC_DRAW);

    //Пятиугольник
    if (is_penta)
        glBufferData(GL_ARRAY_BUFFER, sizeof(pentagon), pentagon, GL_STATIC_DRAW);
    
    //Веер
    if (is_fan)
        glBufferData(GL_ARRAY_BUFFER, sizeof(fan), fan, GL_STATIC_DRAW);


    float color3[3][4] = {
    {1,0,0,1},{0,1,0,1},{0,0,1,1}
    }; //triangle gradient

    float color4[4][4] = {
        {0.8f,0.5f,0.2f,1 },
        {0.1f,1.0f,0.0f,1 },
        {1.0f,0.1f,0.0f,1 },
        {0.0f,1.0f,1.0f,1 },
    }; // quad

    float color5[5][4] = {
        {0.f,1.f,0.f,1 },
        {0.1f,1.0f,0.0f,1 },
        {1.0f,0.1f,0.0f,1 },
        {0.0f,1.0f,1.0f,1 },
        {1,1,1,1}
    }; // penta

    float colorf[9][4] = {
       {0.8f,0.5f,0.2f,1 },
       {0.1f,1.0f,0.0f,1 },
       {1.0f,0.1f,0.0f,1 },
       {0.0f,1.0f,1.0f,1 },
       {1,1,1,1},
       {0.5f,0.2f,1,1},
       {0.3f,0.f,0.99f,1},
       {0.1f,0.1f,0.f,1},
       {1,1,1,1}
    }; // fan

    if (is_gradient) {
        glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
        //tri
        if (is_triag)
            glBufferData(GL_ARRAY_BUFFER, sizeof(color3), color3, GL_STATIC_DRAW);
        //quad
        if (is_quad)
            glBufferData(GL_ARRAY_BUFFER, sizeof(color4), color4, GL_STATIC_DRAW);
        //penta
        if (is_penta)
            glBufferData(GL_ARRAY_BUFFER, sizeof(color5), color5, GL_STATIC_DRAW);
        //fan
        if (is_fan)
            glBufferData(GL_ARRAY_BUFFER, sizeof(colorf), colorf, GL_STATIC_DRAW);
    }
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

    if (is_gradient) {

        glEnableVertexAttribArray(Attrib_color);
        glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
        glVertexAttribPointer(Attrib_color, 4, GL_FLOAT, GL_FALSE, 0, 0);
    }
    glBindBuffer(GL_ARRAY_BUFFER, 0); // Отключаем VBO

    //uniform   
    if (is_uni) {
        float color[4] = { 0.1f,0,0.3f,1.0f };
        location = glGetUniformLocation(Program, "color");
        glUniform4f(location, color[0], color[1], color[2], color[3]);
    }


    if (is_triag)
        glDrawArrays(GL_TRIANGLES, 0, 3); //треугольник
    if (is_quad)
        glDrawArrays(GL_QUADS, 0, 4); // четырехугольник
    if (is_penta)
        glDrawArrays(GL_POLYGON, 0, 5); // пятиугольник
    if (is_fan)
        glDrawArrays(GL_TRIANGLE_FAN, 0, 9);//веер


    glDisableVertexAttribArray(Attrib_vertex); // Отключаем массив атрибутов
    if (is_gradient)
        glDisableVertexAttribArray(Attrib_color); // Отключаем массив атрибутов
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