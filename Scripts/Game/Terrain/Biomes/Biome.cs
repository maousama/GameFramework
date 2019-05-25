using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    internal abstract class Biome
    {
        /// <summary>
        /// 生物群落初始化时候反射出所有非抽象子类为其创建静态单例存入nameToInstance
        /// </summary>
        static Biome()
        {
            var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsClass && type.IsSubclassOf(typeof(Biome)) && !type.IsAbstract
                        select type;
            types.ToList().ForEach((type) =>
            {
                Biome instance = (Biome)Activator.CreateInstance(type);
                nameToInstance.Add(instance.Name, instance);
            });
        }

        private static Dictionary<string, Biome> nameToInstance = new Dictionary<string, Biome>();

        internal static Biome GetBiomeByName(string name)
        {
            return nameToInstance[name];
        }
        internal static string SelectBiome(float temperature, float precipitation)
        {
            if (temperature > -0.1f)
            {
                return GrassLand.BiomeName;
            }
            else return IceLand.BiomeName;
        }

        internal abstract string Name { get; }
        internal abstract string GetBlockByStratum(int stratum);
    }
    //if (temperature < 10)
    //{
    //    if (precipitationPercentage < 50) return ColdDesert.Instance;
    //    else return IceDesert.Instance;
    //}
    //else if (temperature < 30)
    //{
    //    if (precipitationPercentage < 10) return ColdDesert.Instance;
    //    else if (precipitationPercentage < 70) return Tundra.Instance;
    //    else return BorealForest.Instance;
    //}
    //else if (temperature < 50)
    //{
    //    if (precipitationPercentage < 30) return Shrubland.Instance;
    //    else if (precipitationPercentage < 60) return Grassland.Instance;
    //    else return BorealForest.Instance;
    //}
    //else if (temperature < 70)
    //{
    //    if (precipitationPercentage < 10) return Shrubland.Instance;
    //    if (precipitationPercentage < 30) return Grassland.Instance;
    //    else
    //    {
    //        if (height < 0) return Swamp.Instance;
    //        else return Woodland.Instance;
    //    }
    //}
    //else if (temperature < 90)
    //{
    //    if (precipitationPercentage < 30) return Desert.Instance;
    //    else if (precipitationPercentage < 60) return DarkForest.Instance;
    //    else return RainForest.Instance;
    //}
    //else return Lavaland.Instance;
}
