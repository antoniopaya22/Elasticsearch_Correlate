from elasticsearch import Elasticsearch
import json
from pylab import *
import numpy as np

es = Elasticsearch()

# dibuja los puntos de la grafica con titulo "nombre"
def graficar(plt,puntos,nombre):
	graf = plt.plot(puntos,label=nombre)
	return graf


# compara las firmas entre un elemento con firma1 y otro elemento que tiene firma2.
# retorna el numero de caracteres que coinciden en una y en otra firma, en la misma posicion.
# Ejemplo: firma1 = SSSU , firma2 = SSUS  -> retornara 2/4 = 0.5
def compararFirmas(firma1,firma2):
	coinciden = 0
	total = 0
	for i in range(0,len(firma1)):
		palabra1 = firma1[i]
		palabra2 = firma2[i]
		for i in range(0,len(palabra1)):
			letra1 = palabra1[i]
			letra2 = palabra2[i]
			total = total + 1
			if(letra1 == letra2):
				coinciden = coinciden+1
	return coinciden/total


# busca una web con un nombre y un tamaño.
# el tamaño (size) es necesario para los ngramas.
# retornara la firma de la web a buscar
def buscarWeb(nombre,size):
	body = {"query":{"match":{"nombre":nombre}}}
	res = es.search(index = index, body = body)
	firma = ""
	firmaPrincipal = []
	for doc in res['hits']['hits']:
		if(("%s" % (doc['_source']['nombre'])) == nombre):
			firmaCompleta = ("%s" % (doc['_source']['firma'])).split(" ")
			for grama in firmaCompleta:
				if(len(grama) == size):
					firma+=grama+" "
					firmaPrincipal.append(grama)
	return firma,firmaPrincipal


# busca y retorna unicamente aquellas webs que tengan una firma en concreto,
# sin importar el orden de los caracteres.
def buscarWebsParecidas(firma):
	body = {"query":{"match":{"firma":firma}}}
	res = es.search(index = index, body = body)
	webs_parecidas = []
	for doc in res['hits']['hits']:
		webs_parecidas.append(("%s" % (doc['_source']['nombre'])))
	return webs_parecidas


# busca y retorna aquellas webs, de entre las webs obtenidas en el metodo
# buscarWebsParecidas, cuyas firmas, comparadas con la firma (firmaPrincipal) de
# la web que se busca, tengan un porcentaje de relacion > 50%.
# dicho porcentaje se halla con el metodo compararFirmas.
def buscarCorrelacion(firmaPrincipal,webs_parecidas,size):
	firma = []
	websCorrelacionadas = []
	for web in webs_parecidas:
		body = {"query":{"match":{"nombre":web}}}
		res = es.search(index = index, body = body)
		for doc in res['hits']['hits']:
			if(("%s" % (doc['_source']['nombre'])) == web):
				firma = []
				numeroIguales = 0
				firmaCompleta = ("%s" % (doc['_source']['firma'])).split(" ")
				for grama in firmaCompleta:
					if(len(grama) == size):
						firma.append(grama)
				porcent = compararFirmas(firmaPrincipal,firma)
				if(porcent > 0.5):
					websCorrelacionadas.append(web)
					print ("Nombre: ",web)
					print (" Porcentaje: ","{0:.5f}".format(porcent))
	return websCorrelacionadas


# imprime las graficas de dos webs, mostrando ademas una leyenda para indicar
# las graficas que se estan representando.
def imprimirGraficas(web1,web2):
	plt.ylabel('Version normalizada ')
	plt.xlabel('Dia ')
	body = {"query":{"match":{"nombre":web1}}}
	res = es.search(index = index, body = body)
	for doc in res['hits']['hits']:
		if(("%s" % (doc['_source']['nombre'])) == web1):
			values = (doc['_source']['values'])
			graf = graficar(plt,values,web1)

	body = {"query":{"match":{"nombre":web2}}}
	res = es.search(index = index, body = body)
	for doc in res['hits']['hits']:
		if(("%s" % (doc['_source']['nombre'])) == web2):
			values = (doc['_source']['values'])
			graf = graficar(plt,values,web2)
			plt.legend(bbox_to_anchor=(0., 1.02, 1., .102), loc=3,ncol=2, mode="expand", borderaxespad=0.)
	plt.show()


# parte principal del archivo, desde donde se sacan todos los datos
index = "webs-index"
nombre = input("Busqueda (p.ej. google, yahoo, american idol...): ")
ngramas = int(input("Numero ngramas [3, 7]: "))
res = buscarWeb(nombre,ngramas)
firma = res[0]
firmaPrincipal = res[1]
webs = buscarWebsParecidas(firma)
if(len(webs) != 0):
	corr = buscarCorrelacion(firmaPrincipal,webs,ngramas)
	web = input("Escribe el nombre con el que comparar: ")
	imprimirGraficas(nombre,web)
else:
	print("No hay resultados para esa busqueda")
