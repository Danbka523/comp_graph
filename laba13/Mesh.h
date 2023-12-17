#pragma once
#include"Headers.h"
#include<iostream>
#include<string>
#include<sstream>
#include<fstream>
#include<vector>
using namespace std;


static vector<string> split(const string& s, char delim) {
	stringstream ss(s);
	string item;
	vector<string> elems;
	while (getline(ss, item, delim)) {
		if (item.empty()) continue;
		elems.push_back(item);

	}
	return elems;
}


class Mesh {

	vector<float> vertices;
	GLuint VBO;


	void parse_file(const string& path) {
		try {
			ifstream file(path);
			if (!file.is_open()) {
				throw exception("file can't be oppened");
			}

			vector<vector<float>> v;
			vector<vector<float>> vn;
			vector<vector<float>> vt;

			string line;

			while (getline(file, line)) {
				istringstream iss(line);
				string type;
				iss >> type;
				if (type == "v") {
					auto vertex = split(line, ' ');
					vector<float> cv{};
					for (size_t j = 1; j < vertex.size(); j++)
					{
						cv.push_back(stof(vertex[j]));
					}
					v.push_back(cv);

				}
				else if (type == "vn") {
					auto normale = split(line, ' ');
					vector<float> cvn{};
					for (size_t j = 1; j < normale.size(); j++)
					{
						cvn.push_back(stof(normale[j]));
					}
					vn.push_back(cvn);
				}
				else if (type == "vt") {
					auto texture = split(line, ' ');
					vector<float> cvt{};
					for (size_t j = 1; j < texture.size(); j++)
					{
						cvt.push_back(stof(texture[j]));
					}
					vt.push_back(cvt);
				}
				else if (type == "f") {

					auto splitted = split(line, ' ');
					if (splitted.size() < 5) {
						for (size_t i = 1; i < splitted.size(); i++)
						{
							auto triplet = split(splitted[i], '/');
							int positionIndex = stoi(triplet[0]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(v[positionIndex][j]);
							}
							int normaleIndex = stoi(triplet[2]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(vn[normaleIndex][j]);
							}
							int textureIndex = stoi(triplet[1]) - 1;
							for (int j = 0; j < 2; j++) {
								vertices.push_back(vt[textureIndex][j]);
							}
						}
					}
					else {
						vector<vector<string>> verts = {
							split(splitted[1], '/'),
							split(splitted[2], '/'),
							split(splitted[3], '/'),
							split(splitted[4], '/'),
						};

						vector<vector<string>> triang0 = {
							verts[0], verts[1], verts[2]
						};
						for (auto& triplet : triang0) {
							int positionIndex = stoi(triplet[0]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(v[positionIndex][j]);
							}
							int normaleIndex = stoi(triplet[2]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(vn[normaleIndex][j]);
							}
							int textureIndex = stoi(triplet[1]) - 1;
							for (int j = 0; j < 2; j++) {
								vertices.push_back(vt[textureIndex][j]);
							}

						}

						vector<vector<string>> triang1 = {
							verts[0], verts[2], verts[3]
						};
						for (auto& triplet : triang1) {
							int positionIndex = stoi(triplet[0]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(v[positionIndex][j]);
							}
							int normaleIndex = stoi(triplet[2]) - 1;
							for (int j = 0; j < 3; j++) {
								vertices.push_back(vn[normaleIndex][j]);
							}
							int textureIndex = stoi(triplet[1]) - 1;
							for (int j = 0; j < 2; j++) {
								vertices.push_back(vt[textureIndex][j]);
							}
						}
					}

				}
				else {
					continue;
				}
			}
			return;
		}
		catch (const exception& e)
		{
			cerr << e.what() << endl;
		}
		cout << "Verts count:" << vertices.size() << endl;
	}
	
};