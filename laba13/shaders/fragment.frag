#version 330 core
in vec2 otexcoord;
out vec4 fragcolor;

unifrom sampler2d tex;

void main(){
	fragcolor = texture(tex,otexcoord);
}