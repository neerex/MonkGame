using MainGame.Abilities.Spells;
using MainGame.Infrastructure.EntityFactories.SpellFactory;
using MainGame.Infrastructure.EntityFactories.SpellFactory.Interfaces;
using MainGame.ScriptableConfigs;
using Zenject;

namespace MainGame.Installers
{
    public class SpellFactoryInstaller : Installer<SpellFactoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<SpellConfigSO, Spell, Spell.Factory>().AsSingle();
            Container.Bind<ISpellFactory>().To<SpellFactory>().AsSingle();
        }
    }
}