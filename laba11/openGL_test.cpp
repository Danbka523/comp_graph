#define _USE_MATH_DEFINES
#include <GL/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>
#include <cmath>
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
GLuint Attrib_color;
GLuint VBO;
GLuint VBO_color;
GLuint location;

// �������� ��� ���������� �������
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
        vertexColor = vec4(color[0],color[1],color[2],1);
    }
)";


// �������� ��� ������������ �������
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
    // ������ ����
    sf::Window window(sf::VideoMode(600, 600), "laba11", sf::Style::Default, sf::ContextSettings(32));
    // ������ ������ (���� � ��������� ��������� ��� ���������)
    SetIcon(window);
    // �������� ������������ ������������� (������������� ������� ��������� � �������� ������ ��������, ����� �������� �� �������, ������ ��� �� �����������)
    window.setVerticalSyncEnabled(true);

    // ���������� ����
    window.setActive(true);

    // �������������
    glewInit();
    Init();
    // ������� ���� ����
    while (window.isOpen()) {
        
        sf::Event event;
        // ���� ��������� ������� ����, ��� ����� ������ ������� ������, �������� ����� � ������ �����
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed) {
                // ���� �������, ������� �� �����
                window.close();
            }
            else if (event.type == sf::Event::Resized) {
                // ������ ������ ����, ���� �������� � ������ ������� Opengl ���������
                glViewport(0, 0, event.size.width, event.size.height);
            }
        }

        // ������� �������
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // ������ �����
        Draw();

        // ������������ ���� - ������ �������� � ������ ����� �������
        window.display();
    }
    Release();
    return 0;
}


// � ������ ������������� ������� ���������� �������� �������, ������� � ������ �����
void Release() {
    // �������
    ReleaseShader();
    // ��������� �����
    ReleaseVBO();

}
void ReleaseVBO() {
    glBindBuffer(GL_ARRAY_BUFFER, 0);
    glDeleteBuffers(1, &VBO);
}

void ReleaseShader() {
    // ��������� ����, �� ��������� ��������� ���������
    glUseProgram(0);
    // ������� ��������� ���������
    glDeleteProgram(Program);
}

void Init() {
    InitShader();
    InitVBO();
}

void InitShader() {
    // ������� ��������� ������
    GLuint vShader = glCreateShader(GL_VERTEX_SHADER);
    // �������� �������� ���
    // glShaderSource(vShader, 1, &VertexShaderSource, NULL);
    //gradient
    glShaderSource(vShader, 1, &VertexShaderGradientSource, NULL);
    // ����������� ������
    glCompileShader(vShader);
    std::cout << "vertex shader \n";
    // ������� ������ ���� �������
    ShaderLog(vShader);
        // ������� ����������� ������
        GLuint fShader = glCreateShader(GL_FRAGMENT_SHADER);
    // �������� �������� ���
    
    //��� ������� ������� ������������ � �������
    //glShaderSource(fShader, 1, &FragShaderSourceConstColor, NULL);

    //uniform
    //glShaderSource(fShader, 1, &FragShaderSourceUniformColor, NULL); 
    
    //��������
    glShaderSource(fShader, 1, &FragShaderSourceGradientColor, NULL);




    // ����������� ������
    glCompileShader(fShader);
    std::cout << "fragment shader \n";
    // ������� ������ ���� �������
    ShaderLog(fShader);
        // ������� ��������� � ����������� ������� � ���
        Program = glCreateProgram();
    glAttachShader(Program, vShader);
    glAttachShader(Program, fShader);
    // ������� ��������� ���������
    glLinkProgram(Program);
    // ��������� ������ ������
    int link_ok;
    glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
    if (!link_ok) {
        std::cout << "error attach shaders \n";
        return;
    }
        // ���������� ID �������� �� ��������� ���������
        const char* attr_name = "coord"; //��� � �������
    Attrib_vertex = glGetAttribLocation(Program, attr_name);
    if (Attrib_vertex == -1) {
        std::cout << "could not bind attrib " << attr_name << std::endl;
        return;
    }

            const char* attr_name1 = "color"; //��� � �������
    Attrib_vertex = glGetAttribLocation(Program, attr_name1);
    if (Attrib_vertex == -1) {
        std::cout << "could not bind color " << attr_name << std::endl;
        return;
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

    float radius = 1.0f;
    float angleinc = 360.f / 5.0f;
    angleinc *= M_PI / 180.0f;
    float angle = 180.0f;
    Vertex pentagon[5] = {
        {radius * cos(angle + angleinc * 1),radius * sin(angle + angleinc * 1)},
        {radius * cos(angle + angleinc * 2),radius * sin(angle + angleinc * 2)},
        {radius * cos(angle + angleinc * 3),radius * sin(angle + angleinc * 3)},
        {radius * cos(angle + angleinc * 4),radius * sin(angle + angleinc * 4)},
        {radius * cos(angle + angleinc * 5),radius * sin(angle + angleinc * 5)},


    };

    //Vertex triangleFan[] = {
    //    {-0.5f,-0.5f},
    //    {-0.5f,0.5f},
    //    {-0.5f,1.0f},
    //    {-0.5f,-0.5f},
    //    {-0.5f,-0.5f},
    //    {-0.5f,-0.5f},
    //}


    glBindBuffer(GL_ARRAY_BUFFER, VBO);

    //�����������
    glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
    
    //���������������
    //glBufferData(GL_ARRAY_BUFFER, sizeof(quadrilateral), quadrilateral, GL_STATIC_DRAW);

    //������������
    //glBufferData(GL_ARRAY_BUFFER, sizeof(pentagon), pentagon, GL_STATIC_DRAW);
    
    float color3[3][4] = {
    {1,0,0,1},{0,1,0,1},{0,0,0.5f,1}
    };
    glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
    glBufferData(GL_ARRAY_BUFFER, sizeof(color3), color3, GL_STATIC_DRAW);

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

// ������� ��������������� ��������� �����
void Draw() {
    glUseProgram(Program); // ������������� ��������� ��������� �������

    glEnableVertexAttribArray(Attrib_vertex); // �������� ������ ���������
    glBindBuffer(GL_ARRAY_BUFFER, VBO); // ���������� VBO
    // �������� pointer 0 ��� ������������ ������, �� ��������� ��� ������ � VBO
    glVertexAttribPointer(Attrib_vertex, 2, GL_FLOAT, GL_FALSE, 0, 0);

    glEnableVertexAttribArray(Attrib_color);
    glBindBuffer(GL_ARRAY_BUFFER, VBO_color);
    glVertexAttribPointer(Attrib_color, 4, GL_FLOAT, GL_FALSE, 0, 0);

    glBindBuffer(GL_ARRAY_BUFFER, 0); // ��������� VBO

    //uniform   
    /*float color[4] = { 0.1f,0,0.3f,1.0f };
    location = glGetUniformLocation(Program,"color");
    glUniform4f(location, color[0], color[1], color[2], color[3]);*/



    glDrawArrays(GL_TRIANGLES, 0, 3); //�����������

    //glDrawArrays(GL_QUADS, 0, 4); // ���������������
    //glDrawArrays(GL_POLYGON, 0, 5); // ������������

    glDisableVertexAttribArray(Attrib_vertex); // ��������� ������ ���������
    glDisableVertexAttribArray(Attrib_color); // ��������� ������ ���������
    glUseProgram(0); // ��������� ��������� ���������
    checkOpenGLerror();
}


void SetIcon(sf::Window& wnd)
{
    sf::Image image;

    // ������ ��������� ��������, ����� ��������� ������ �� ����� (image.loadFromFile("icon.png"))
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