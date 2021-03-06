//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        public NetherWars.ManaCost manaCost { get { return (NetherWars.ManaCost)GetComponent(ComponentIds.ManaCost); } }

        public bool hasManaCost { get { return HasComponent(ComponentIds.ManaCost); } }

        public Entity AddManaCost(int newValue, System.Collections.Generic.Dictionary<NetherWars.eColorType, int> newThrashold) {
            var component = CreateComponent<NetherWars.ManaCost>(ComponentIds.ManaCost);
            component.Value = newValue;
            component.Thrashold = newThrashold;
            return AddComponent(ComponentIds.ManaCost, component);
        }

        public Entity ReplaceManaCost(int newValue, System.Collections.Generic.Dictionary<NetherWars.eColorType, int> newThrashold) {
            var component = CreateComponent<NetherWars.ManaCost>(ComponentIds.ManaCost);
            component.Value = newValue;
            component.Thrashold = newThrashold;
            ReplaceComponent(ComponentIds.ManaCost, component);
            return this;
        }

        public Entity RemoveManaCost() {
            return RemoveComponent(ComponentIds.ManaCost);
        }
    }

    public partial class Matcher {
        static IMatcher _matcherManaCost;

        public static IMatcher ManaCost {
            get {
                if (_matcherManaCost == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ManaCost);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherManaCost = matcher;
                }

                return _matcherManaCost;
            }
        }
    }
}
