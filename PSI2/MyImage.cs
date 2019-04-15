﻿using System;
using System.Diagnostics;
using System.IO;
// Kevin LIM et Thomas NGO TD G
namespace PSI2
{
    class MyImage
    {
        #region Attributs
        private string imageType;
        private int tailleFichier;
        private int tailleOffset;
        private int largeur;
        private int hauteur;
        private int nbBitsCouleur;
        private Pixel[,] image;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Déclaration du "Constructeur 1"
        /// </summary>
        /// <param name="fileName"></param>
        public MyImage(string fileName)
        {
            byte[] myfile = null;
            //Process.Start(fileName); // Affiche le fichier
            if (fileName[fileName.Length - 1] == 'v')
            {
                myfile = TraitementCSV(fileName);
            }
            else
            {
                myfile = File.ReadAllBytes(fileName);
            }
            // ● type d’image (BM par exemple),  
            if (myfile[0] == 66 && myfile[1] == 77)
            {
                this.imageType = "bmp";
            }
            // ● taille du fichier (int)
            byte[] tab = new byte[4];
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 2];
            }
            this.tailleFichier = Convertir_Endian_To_Int(tab);
            Console.WriteLine("Taille du fichier : " + tailleFichier);
            Console.WriteLine("taille data" + myfile.Length);
            // ● taille Offset (int)
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 10];
            }
            this.tailleOffset = Convertir_Endian_To_Int(tab);
            Console.WriteLine("Taille offset : " + tailleOffset);

            // ● largeur et hauteur de l’image (int) 
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 18];
            }
            this.largeur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("largeur : " + largeur);
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 22];
            }
            this.hauteur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("hauteur : " + hauteur);


            // ● nombre de bits par couleur(int)  
            for (int index = 0; index < 2; index++)
            {
                tab[index] = myfile[index + 28];
            }
            this.nbBitsCouleur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("nbbc : " + nbBitsCouleur);


            // ● l’image par elle-même sur laquelle vous ferez les traitements proposés ensuite. (matrice de RGB) 
            int indexColonne = 0;
            int indexLigne = 0;

            image = new Pixel[hauteur, largeur]; // Affectation de la taille de la matrice
            for (int index = 54; index <= (tailleFichier - 3); index += 3) // Lecture des pixels ( 3 bytes )
            {

                if (indexColonne == (largeur))
                {
                    indexColonne = 0;
                    indexLigne++;
                }
                Pixel data = new Pixel(myfile[index], myfile[index + 1], myfile[index + 2]);
                image[indexLigne, indexColonne] = data;

                //Console.WriteLine("Index: " + index + "; " + myfile[index] + " " + myfile[index + 1] + " " + myfile[index + 2]);

                indexColonne++;
            }
        }
        /// <summary>
        /// Déclaration du "Constructeur 2"
        /// </summary>
        /// <param name="largeur"></param>
        /// <param name="hauteur"></param>
        public MyImage(string filename, int largeur, int hauteur)
        {
            Mandelbrot(hauteur, largeur);
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// 
        /// </summary>
        public Pixel[,] Image
        {
            get { return image; }
            set { image = value; }
        }
        #endregion

        #region Methodes_Affichage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public void AfficherTableauPixel(Pixel[,] image)
        {
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Console.Write(image[i, j].ToString() + " ; ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        public void AfficherTableauByte(byte[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i]);
            }
        }
        public void AfficherTableauInt(int[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        public void AfficherTableauString(string[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Console.Write(tab[i] + " ");
            }
        }
        #endregion

        #region Méthodes

        #region TD1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void ToString(string fileName)
        {
            Console.WriteLine("Lancement to string");
            byte[] myfile = File.ReadAllBytes(fileName);
            for (int i=0;i<myfile.Length;i++)
            {
                Console.Write(myfile[i]);
            }
            Console.WriteLine();

            // ● taille du fichier (int)
            byte[] tab = new byte[4];
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 2];
            }
            this.tailleFichier = Convertir_Endian_To_Int(tab);
            Console.WriteLine("Taille du fichier : " + tailleFichier);


            // ● taille Offset (int)
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 10];
            }
            this.tailleOffset = Convertir_Endian_To_Int(tab);
            Console.WriteLine("Taille offset : " + tailleOffset);


            // ● largeur et hauteur de l’image (int) 
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 18];
            }
            this.largeur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("largeur : " + largeur);
            for (int index = 0; index < 4; index++)
            {
                tab[index] = myfile[index + 22];
            }
            this.hauteur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("hauteur : " + hauteur);


            // ● nombre de bits par couleur(int)  
            for (int index = 0; index < 2; index++)
            {
                tab[index] = myfile[index + 28];
            }
            this.nbBitsCouleur = Convertir_Endian_To_Int(tab);
            Console.WriteLine("nbbc : " + nbBitsCouleur);
        }

        /// <summary>
        /// Permet de traiter créer le tableau de byte à partir d'un CSV
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] TraitementCSV(string fileName)
        {
            byte[] bytepixel = null;
            try
            {
                StreamReader lecture = new StreamReader(fileName);
                string ligne = "";
                string chaine_pixel = "";
                int lignefichier = 0; //Permet de savoir la ligne lue dans le fichier
                while (lecture.EndOfStream == false)
                {
                    lignefichier++;
                    ligne = lecture.ReadLine();

                    if (lignefichier == 1) //Lecture de la première ligne (header1)
                    {
                        string[] temp = ligne.Split(';');
                        for (int j = 0; j < 14; j++) { chaine_pixel += temp[j] + ';'; }
                    }
                    else
                    {
                        if (lignefichier == 2) //Lectur de la deuxiemeligne (header2)
                        {
                            string[] temp = ligne.Split(';');
                            for (int j = 0; j < 40; j++) { chaine_pixel += temp[j] + ';'; }
                        }
                        else
                        {
                            image = new Pixel[hauteur, largeur];
                            string[] temp = ligne.Split(';');
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if (temp[i] != "")
                                {
                                    chaine_pixel += temp[i] + ';';
                                }
                            }
                        }
                    }
                    
                }
                lecture.Close();
                string[] tabpixel = chaine_pixel.Split(';');
                bytepixel = new byte[tabpixel.Length];
                for (int i = 0; i < tabpixel.Length - 1; i++)
                {
                    byte temp = Convert.ToByte(tabpixel[i]);
                    bytepixel[i] = Convert.ToByte(tabpixel[i]);
                }
                AfficherTableauByte(bytepixel);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return bytepixel;
        }

        /// <summary>
        /// Affiche le header, le header info et l'image sur la console
        /// </summary>
        /// <param name="myfile">l'image à afficher</param>
        public void AffichageFichier(string fileName)
        {
            Console.WriteLine("HEADER");
            Console.WriteLine();
            Console.WriteLine();
            byte[] myfile = File.ReadAllBytes(fileName);
            for (int i = 0; i < 14; i++)
            {
                Console.Write(myfile[i] + " ");
            }
            Console.WriteLine("\n\nHEADER INFO\n\n");
            for (int i = 14; i < 54; i++)
            {
                
                Console.Write(myfile[i] + " ");

            }
            Console.WriteLine("\n\nIMAGE\n");
            for (int i = 54; i < myfile.Length; i++)
            {
                Console.Write(myfile[i] + "\t");
            }
        }

        /// <summary>
        /// Convertit une séquence d’octet au format little endian en entier
        /// </summary>
        /// <param name="tab">Séquence d'octet</param>
        /// <returns>L'entier correspondant à la séquence d'octet</returns>
        public int Convertir_Endian_To_Int(byte[] tab)
        {
            string[] data1 = new string[tab.Length]; // Tableau de string pour travailler avec les hexadecimale
            for (int index = 0; index < tab.Length; index++)
            {
                data1[index] = tab[index].ToString("X"); // Conversion du tableau de decimal dans un tableau d'hexadecimale
            }
            Array.Reverse(data1); // Inverser les valeurs d'un tableau : La valeur en index 0 va en index data.Length-1
            string data2 = string.Join("", data1); // Convertit le tableau d'hexadecimale sur 4 octet en hexadecimale sur 1 octet
            int result = Convert.ToInt32(data2, 16); // Convertit l'hexadecimal en int
            return result;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] intBytes = BitConverter.GetBytes(val);

            return intBytes;
        }

        /// <summary>
        /// prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// </summary>
        /// <param name="fileName">nom du fichier traité</param>
        public void From_Image_To_File(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string ImageToByte = "ImageToByte.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i-2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i-6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i-10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i-14];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i-18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i-22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i-28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;
            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
                {
                    
                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur+1] = image[indexLigne, indexColonne].Green;
                    data[compteur+2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;
                }
            }

            File.WriteAllBytes(ImageToByte, data);

            Process.Start(ImageToByte);
        }
        #endregion
        
        #region TD2
        public void Noir_Blanc(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Noir_Blanc = "Noir_Blanc.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }


            //Image
            int compteur = 54;
            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
                {
                    byte datatmp = Convert.ToByte((image[indexLigne, indexColonne].Red + image[indexLigne, indexColonne].Green + image[indexLigne, indexColonne].Blue) / 3);
                    data[compteur] = datatmp;
                    data[compteur + 1] = datatmp;
                    data[compteur + 2] = datatmp;
                    compteur = compteur + 3;
                }
            }

            File.WriteAllBytes(Noir_Blanc, data);

            Process.Start(Noir_Blanc);
        }

        public void Effet_Miroir(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Effet_Miroir = "Effet_Miroir.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;

            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++) //Parcours la matrice du coin haut gauche [0,0]
            {
                for (int indexColonne = (image.GetLength(1) - 1); indexColonne >= 0; indexColonne--)
                {

                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;
                }
            }


            File.WriteAllBytes(Effet_Miroir, data);
            Process.Start(Effet_Miroir);

        }

        public void Rotation_90(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Rotation = "Rotation.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;

            for (int indexColonne = (image.GetLength(1) - 1); indexColonne >= 0; indexColonne--)
            {
                for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++)
                {
                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;

                }
            }

            File.WriteAllBytes(Rotation, data);
            Process.Start(Rotation);

        }

        public void Rotation_180(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Rotation_180 = "Rotation.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;

            for (int indexLigne = (image.GetLength(0) - 1); indexLigne >= 0; indexLigne--)
            {
                for (int indexColonne = (image.GetLength(1) - 1); indexColonne >= 0; indexColonne--)
                {
                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;
                }
            }


            File.WriteAllBytes(Rotation_180, data);
            Process.Start(Rotation_180);
        }

        public void Rotation_270(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Rotation = "Rotation.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;

            for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
            {
                for (int indexLigne = (image.GetLength(0) - 1); indexLigne >= 0; indexLigne--)
                {
                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;
                }
            }


            File.WriteAllBytes(Rotation, data);
            Process.Start(Rotation);
        }

        public void Agrandir(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Agrandir = "Agrandir.bmp";
            byte[] data = new byte[tailleOffset+(2*largeur*2*hauteur)*3];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleOffset + (largeur * hauteur) * 3);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(largeur*2);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur*2);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;
            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
                {

                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;

                    data[compteur + 3] = image[indexLigne, indexColonne].Red;
                    data[compteur + 4] = image[indexLigne, indexColonne].Green;
                    data[compteur + 5] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 6;
                }
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
                {

                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;

                    data[compteur + 3] = image[indexLigne, indexColonne].Red;
                    data[compteur + 4] = image[indexLigne, indexColonne].Green;
                    data[compteur + 5] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 6;
                }
            }

            File.WriteAllBytes(Agrandir, data);

            Process.Start(Agrandir);
        }

        public void Retrecir(string fileName)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string Retrecir = "Retrecir.bmp";
            byte[] data = new byte[tailleOffset + (largeur / 2 * hauteur / 2) * 3];

            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(Convert.ToInt32(tailleOffset + ((largeur / 2) * (hauteur / 2)) * 3));

            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(Convert.ToInt32(largeur / 2));
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(Convert.ToInt32(hauteur / 2));
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int compteur = 54;
            /*
            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne = indexLigne+2)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne = indexColonne + 2)
                {
                    byte pixel1 = Convert.ToByte((image[indexLigne, indexColonne].Red + image[indexLigne, indexColonne].Green + image[indexLigne, indexColonne].Blue) / 3);
                    byte pixel2 = Convert.ToByte((image[indexLigne, indexColonne + 1].Red + image[indexLigne, indexColonne + 1].Green + image[indexLigne, indexColonne + 1].Blue) / 3);
                    byte pixel3 = Convert.ToByte((image[indexLigne + 1, indexColonne].Red + image[indexLigne + 1, indexColonne].Green + image[indexLigne + 1, indexColonne].Blue) / 3);
                    byte pixel4 = Convert.ToByte((image[indexLigne + 1, indexColonne + 1].Red + image[indexLigne + 1, indexColonne + 1].Green + image[indexLigne + 1, indexColonne + 1].Blue) / 3);

                    byte max = pixel1;
                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;

                    if (pixel2 > max)
                    {
                        max = pixel2;
                        data[compteur] = image[indexLigne, indexColonne+1].Red;
                        data[compteur + 1] = image[indexLigne, indexColonne+1].Green;
                        data[compteur + 2] = image[indexLigne, indexColonne+1].Blue;
                    }

                    if (pixel3 > max)
                    {
                        max = pixel3;
                        data[compteur] = image[indexLigne+1, indexColonne].Red;
                        data[compteur + 1] = image[indexLigne+1, indexColonne].Green;
                        data[compteur + 2] = image[indexLigne+1, indexColonne].Blue;
                    }

                    if (pixel4 > max)
                    {
                        data[compteur] = image[indexLigne+1, indexColonne + 1].Red;
                        data[compteur + 1] = image[indexLigne+1, indexColonne + 1].Green;
                        data[compteur + 2] = image[indexLigne+1, indexColonne + 1].Blue;
                    }

                    compteur = compteur+3;
                }
 
            }
            */

            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne = indexLigne + 2)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne = indexColonne + 2)
                {

                    data[compteur] = image[indexLigne, indexColonne].Red;
                    data[compteur + 1] = image[indexLigne, indexColonne].Green;
                    data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                    compteur = compteur + 3;
                }
            }

            File.WriteAllBytes(Retrecir, data);

            Process.Start(Retrecir);
        }
        #endregion
        
        #region TD3
        public void Convolution (string fileName, string effet)
        {
            byte[] myfile = File.ReadAllBytes(fileName);
            string convolution = "Convolution.bmp";
            byte[] data = new byte[myfile.Length];
            byte[] tempdata = new byte[4];

            //Header
            data[0] = 66;
            data[1] = 77;

            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int i = 2; i <= 5; i++)
            {
                data[i] = tempdata[i - 2];
            }

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            tempdata = Convertir_Int_To_Endian(tailleOffset);
            for (int i = 10; i <= 13; i++)
            {
                data[i] = tempdata[i - 10];
            }

            //HeaderInfo
            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            tempdata = Convertir_Int_To_Endian(largeur);
            for (int i = 18; i <= 21; i++)
            {
                data[i] = tempdata[i - 18];
            }

            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int i = 22; i <= 25; i++)
            {
                data[i] = tempdata[i - 22];
            }

            data[26] = 0;
            data[27] = 0;

            tempdata = Convertir_Int_To_Endian(nbBitsCouleur);
            for (int i = 28; i <= 29; i++)
            {
                data[i] = tempdata[i - 28];
            }

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }

            //Image
            int[,] effetConvolution = null;
            if(effet == "detection")
            {
                int[,] detection = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                effetConvolution = detection;
            }
            if (effet == "renforcement")
            {
                int[,] renforcement = new int[,] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                effetConvolution = renforcement;
            }
            if (effet == "flou")
            {
                //int[,] flou = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                int[,] flou = new int[,] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                effetConvolution = flou;
            }
            if (effet == "repoussage")
            {
                int[,] repoussage = new int[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                effetConvolution = repoussage;
            }

            int compteur = 54;
            for (int indexLigne = 0; indexLigne < image.GetLength(0); indexLigne++)
            {
                for (int indexColonne = 0; indexColonne < image.GetLength(1); indexColonne++)
                {
                    if(indexLigne == 0 || indexColonne == 0 || indexLigne == (image.GetLength(0) - 1) || indexColonne == (image.GetLength(1) - 1))
                    {
                        data[compteur] = image[indexLigne, indexColonne].Red;
                        data[compteur + 1] = image[indexLigne, indexColonne].Green;
                        data[compteur + 2] = image[indexLigne, indexColonne].Blue;
                        compteur = compteur + 3;
                    }
                    else
                    {
                        Pixel pixelTemp = ApplicationConvolution(image, effetConvolution, indexLigne, indexColonne);
                        data[compteur] = pixelTemp.Red;
                        data[compteur + 1] = pixelTemp.Green;
                        data[compteur + 2] = pixelTemp.Blue;
                        compteur = compteur + 3;
                    }
                    
                }
            }

            File.WriteAllBytes(convolution, data);

            Process.Start(convolution);

        }
        public Pixel ApplicationConvolution(Pixel[,] image, int[,] matrice_convolution, int x, int y)
        {
            byte red=0;
            byte green=0;
            byte blue=0;
            int dataRed = 0;
            int dataGreen = 0;
            int dataBlue = 0;

            for (int i = 0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    dataRed += red + (image[(x-1) + i, (y-1) + j].Red)* matrice_convolution[i, j];
                    dataGreen += green + (image[(x - 1) + i, (y-1) + j].Green) * matrice_convolution[i, j];
                    dataBlue += blue + (image[(x - 1) + i, (y-1) + j].Blue) * matrice_convolution[i, j];

                }
            }
            if (dataRed > 255)
            {
                red = 255;
            }
            else if (dataRed < 0)
            {
                red = 0;
            }
            else
            {
                red = Convert.ToByte(dataRed);
            }

            if (dataGreen > 255)
            {
                green = 255;
            }
            else if (dataGreen < 0)
            {
                green = 0;
            }
            else
            {
                green = Convert.ToByte(dataGreen);
            }

            if (dataBlue > 255)
            {
                blue = 255;
            }
            else if (dataBlue < 0)
            {
                blue = 0;
            }
            else
            {
                blue = Convert.ToByte(dataBlue);
            }


            Pixel pixelTemp = new Pixel(red, green, blue);

            return pixelTemp;
        }
        #endregion
        
        #region TD4
        public byte[] CreerBMP(string filename, int largeur, int hauteur)
        {
            int tailleFichier = largeur * hauteur * 3 + 54;
            byte[] data = new byte[tailleFichier];
            byte[] tempdata = new byte[4];
            data[0] = 66;
            data[1] = 77;

            //Définition de la taille du fichier
            tempdata = Convertir_Int_To_Endian(tailleFichier);
            for (int index = 2; index <= 5; index++)
            {
                data[index] = tempdata[index - 2];
                //Console.Write(data[index] + " ");
            }
            Console.WriteLine("Taille du fichier : " + tailleFichier);

            tempdata = Convertir_Int_To_Endian(0);
            for (int i = 6; i <= 9; i++)
            {
                data[i] = tempdata[i - 6];
            }

            //Définiton de la taille de l'offset
            int Offset = 54;
            tempdata = Convertir_Int_To_Endian(Offset);
            for (int index = 10; index <= 13; index++)
            {
                data[index] = tempdata[index - 10];
                //Console.Write(data[index] + " ");
            }
            Console.WriteLine("Taille de l'offset : " + Offset);

            tempdata = Convertir_Int_To_Endian(40);
            for (int i = 14; i <= 17; i++)
            {
                data[i] = tempdata[i - 14];
            }

            //Définition de la largeur
            tempdata = Convertir_Int_To_Endian(largeur);
            for (int index = 18; index <= 21; index++)
            {
                data[index] = tempdata[index - 18];
                //Console.Write(data[index] + " ");
            }
            Console.WriteLine("largeur : " + largeur);
            tempdata = Convertir_Int_To_Endian(hauteur);
            for (int index = 22; index <= 25; index++)
            {
                data[index] = tempdata[index - 22];
                //Console.Write(data[index] + " ");
            }
            Console.WriteLine("hauteur : " + hauteur);

            data[26] = 0;
            data[27] = 0;

            //Définition Nb de bits couleur
            int nbbits = 24;
            tempdata = Convertir_Int_To_Endian(nbbits);
            for (int index = 28; index <= 30; index++)
            {
                data[index] = tempdata[index - 28];
                //Console.Write(data[index] + " ");
            }
            Console.WriteLine("nbbits : " + nbbits);

            for (int i = 30; i <= 53; i++)
            {
                data[i] = 0;
            }
            File.WriteAllBytes(filename, data);
            //AfficherTableauByte(data);
            Process.Start(filename);
            return data;
        }
        public void Mandelbrot(int hauteur, int longueur)
        {
            Console.WriteLine("Création de la fractale");
            string filename = "fractale.bmp";
            byte[] data = CreerBMP(filename, longueur, hauteur);
            Console.ReadKey();
            Pixel[,] image = new Pixel[longueur, largeur];
            //Console.ReadKey();
            for (int  i=0; i<hauteur;i++)
            {
                for (int j=0;j<longueur;j++)
                {
                    double a = (double)(i - hauteur / 2) / (double)(hauteur / 4);
                    
                    double b = (double)(j - longueur / 2) / (double)(longueur / 4);
                    int iteration = 0;
                    while (iteration <100||a+b>16)
                    {
                        double aa = a * a - b * b;
                        double bb = 2.0 * a * b;
                        a = aa + a;
                        b = bb + b;
                        iteration++;
                    }
                    image[i,j] = new Pixel(255,255,255);
                }
            }
            byte[] imgbyte = ConvertirPixelMatrice(image);
            for (int i=54; i<data.Length;i++)
            {
                data[i] = imgbyte[i - 54];
            }
            AfficherTableauByte(data);
            File.WriteAllBytes(filename, data);
            Process.Start(filename);
        }
        public byte[] ConvertirPixelMatrice(Pixel[,] image)
        {
            byte[] bytearray = new byte[(image.GetLength(0) + image.GetLength(1))*3];
            for (int i=0; i<image.GetLength(0);i++)
            {
                for (int j = 0; j < image.GetLength(1); i++)
                {
                    bytearray[i + j] = image[i,j].Red;
                    bytearray[i + j + 1] = image[i, j].Green;
                    bytearray[i + j + 2] = image[i, j].Blue;
                }
            }
            AfficherTableauByte(bytearray);
            return bytearray;
        }
        public void RemplirNoir (Pixel[,] image)
        {
            for (int i=0;i<image.GetLength(0);i++)
            {
                for (int j=0;j<image.GetLength(1);j++)
                {
                    image[i, j] = new Pixel(0, 0, 0);
                }
            }
        }
        public void Histograme()
        {
            int[] histogram_r = new int[256];
            int[] histogram_g = new int[256];
            int[] histogram_b = new int[256];
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    histogram_r[image[i, j].Red]++;
                    histogram_g[image[i, j].Green]++;
                    histogram_b[image[i, j].Blue]++;
                }
            }
            AfficherTableauInt(histogram_r);
            Console.WriteLine();
            AfficherTableauInt(histogram_g);
            Console.WriteLine();
            AfficherTableauInt(histogram_g);
            Console.WriteLine();

            Pixel[,] histo_r = new Pixel[hauteur * largeur, 256];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < histogram_r[i]; j++)
                {
                    histo_r[i, j] = new Pixel(255, 0, 0);
                }
            }
            Pixel[,] histo_g = new Pixel[hauteur * largeur, 256];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < histogram_g[i]; j++)
                {
                    histo_g[i, j] = new Pixel(255, 0, 0);
                }
            }
            Pixel[,] histo_b = new Pixel[hauteur * largeur, 256];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < histogram_b[i]; j++)
                {
                    histo_b[i, j] = new Pixel(255, 0, 0);
                }
            }
        }
        #endregion

        #endregion
    }
}
