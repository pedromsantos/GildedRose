using System.Collections.Generic;
using NUnit.Framework;
using FsCheck;
using GildedRose;

namespace GildedRoseTests
{
    [TestFixture]
    public class GildedRoseShould
    {
       [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntGreaterThanOne) })]
       public void Decrease_unnamed_item_quality_by_two_when_quality_is_above_two_and_sell_in_is_zero(PositiveInt quality)
        {
            var items = new List<Item> { new Item { Name = "", Quality = quality.Get, SellIn = 0 } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == quality.Get - 2);
        }

        [FsCheck.NUnit.Property]
        public void Decrease_item_sellin_when_item_name_is_not_sulfuras(PositiveInt sellIn, NonEmptyString itemName)
        {
            var items = new List<Item> {new Item {Name = itemName.Get, Quality = 0, SellIn = sellIn.Get} };
            var gildedRose = new GildedRose.GildedRose(items);
            var previousSellIn = items[0].SellIn;

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].SellIn < previousSellIn);
        }

        [FsCheck.NUnit.Property]
        public void Decrease_item_sellin_when_item_name_is_aged_brie(PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Aged Brie", Quality = 0, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);
            var previousSellIn = items[0].SellIn;

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].SellIn < previousSellIn);
        }

        [FsCheck.NUnit.Property]
        public void Decrease_item_sellin_when_item_name_is_backstage_passes(PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 0, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);
            var previousSellIn = items[0].SellIn;

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].SellIn < previousSellIn);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntSmallerThanFortyNine) })]
        public void Increase_aged_brie_quality_by_two_when_quality_is_between_one_and_forty_nine_and_sell_in_is_zero(PositiveInt quality)
        {
            var items = new List<Item> {new Item {Name = "Aged Brie", Quality = quality.Get, SellIn = 0}};
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == quality.Get + 2);
        }

        [FsCheck.NUnit.Property]
        public void Set_backstage_passes_quality_to_zero_when_sell_in_is_zero(PositiveInt quality)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = quality.Get, SellIn = 0 } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == 0);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntGreaterThanEleven)})]
        public void Increase_backstage_passes_quality_by_one_when_quality_is_below_50_and_sell_in_is_above_11(PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 25, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == 26);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntBtween6And11) })]
        public void Increase_backstage_passes_quality_by_two_when_sell_in_is_between_6_and_11(PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 25, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == 27);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntSmallerThan6) })]
        public void Increase_backstage_passes_quality_by_three_when_sell_in_is_bellow_6(PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 25, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == 28);
        }

        [FsCheck.NUnit.Property(MaxTest = 45)]
        public void not_decrease_sulfuras_quality(PositiveInt quality, PositiveInt sellIn)
        {
            var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", Quality = quality.Get, SellIn = sellIn.Get } };
            var gildedRose = new GildedRose.GildedRose(items);

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].Quality == quality.Get);
        }

        [FsCheck.NUnit.Property(Arbitrary = new[] { typeof(PositiveIntGreaterThanOne) })]
        public void Not_decrease_sulfuras_sellin_when_quality_above_two(PositiveInt quality)
        {
            var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", Quality = quality.Get, SellIn = 0 } };
            var gildedRose = new GildedRose.GildedRose(items);
            var previousSellIn = items[0].SellIn;

            gildedRose.UpdateQuality();

            Check.QuickThrowOnFailure(items[0].SellIn == previousSellIn);
        }
    }

    public static class PositiveIntGreaterThanOne
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get > 1);
        }
    }


    public static class PositiveIntSmallerThanFortyNine
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get < 49);
        }
    }

    public static class PositiveIntGreaterThanOneAnSmallerThanFortyNine
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get > 1 && x.Get < 49);
        }
    }

    public static class PositiveIntGreaterThanEleven
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get > 11);
        }
    }

    public static class PositiveIntSmallerThan6
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get < 6);
        }
    }

    public static class PositiveIntBtween6And11
    {
        public static Arbitrary<PositiveInt> Ints()
        {
            return Arb.Default.PositiveInt().Filter(x => x.Get > 5 && x.Get < 11);
        }
    }
}